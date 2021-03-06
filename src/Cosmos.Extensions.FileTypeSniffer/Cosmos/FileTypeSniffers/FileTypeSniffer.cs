﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cosmos.FileTypeSniffers.Core;
using Cosmos.FileTypeSniffers.Core.Extensions;

namespace Cosmos.FileTypeSniffers
{
    public class FileTypeSniffer : IFileTypeSniffer
    {
        private readonly List<SniffingMetadata> _complexMetadata;
        private readonly SniffingMetadataStatistics _metadataStatistics;
        private SniffingNode _root;

        public FileTypeSniffer()
        {
            _root = new SniffingNode(default, -1)
            {
                Children = new SortedList<byte, SniffingNode>(128)
            };
            _complexMetadata = new List<SniffingMetadata>();
            _metadataStatistics = new SniffingMetadataStatistics();
        }

        public List<string> Match(byte[] data, bool matchAll = false)
        {
            var ret = _root.Match(0, data, matchAll);

            if (matchAll || !ret.Any())
            {
                ret.AddRange(_complexMetadata.Match(data, matchAll));
            }

            return ret.Distinct().ToList();
        }

        public List<string> Match(string filePath, int simpleLength, bool matchAll = false)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath));

            if (simpleLength <= 0)
                throw new ArgumentOutOfRangeException(nameof(simpleLength), simpleLength, "Simple length must greater than 0");

            if (!File.Exists(filePath))
                throw new IOException($"File '{filePath}' does not exist.");

            var bytes = new byte[simpleLength];
            using (var file = File.OpenRead(filePath))
            {
                file.Read(bytes, 0, bytes.Length);
            }

            return Match(bytes, matchAll);
        }

        public string MatchSingle(byte[] data) => Match(data).FirstOrDefault();

        public string MatchSingle(string filePath, int simpleLength) => Match(filePath, simpleLength).FirstOrDefault();

        public IFileTypeSniffer Expect(List<string> expectedResults) => new ExpectedFileTypeSniffer(this, expectedResults);

        public IFileTypeSniffer Expect(string expectedResult) => new ExpectedFileTypeSniffer(this, expectedResult);

        internal void Register(SniffingMetadata metadata)
        {
            if (metadata.IsComplexMetadata)
            {
                _complexMetadata.Add(metadata);
                _metadataStatistics.UpdateComplexMetadata(metadata.ExtensionNames.Count);
            }
            else
            {
                _root.Update(metadata.HexBytes, metadata.ExtensionNames, 0);
                _metadataStatistics.UpdateSimpleMetadata(metadata.ExtensionNames.Count);
            }
        }

        public SniffingReadOnlyMetadataStatistics GetMetadataStatistics() => new SniffingReadOnlyMetadataStatistics(_metadataStatistics);
    }
}