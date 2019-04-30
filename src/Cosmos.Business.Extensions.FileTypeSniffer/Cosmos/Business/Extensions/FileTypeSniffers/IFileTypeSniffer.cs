﻿using System.Collections.Generic;
using Cosmos.Business.Extensions.FileTypeSniffers.Core;

namespace Cosmos.Business.Extensions.FileTypeSniffers
{
    public interface IFileTypeSniffer
    {
        List<string> Match(byte[] data, bool matchAll = false);

        List<string> Match(string filePath, int simpleLength, bool matchAll = false);

        string MatchSingle(byte[] data);

        string MatchSingle(string filePath, int simpleLength);

        IFileTypeSniffer Expect(List<string> expectedResults);

        IFileTypeSniffer Expect(string expectedResult);

        SniffingReadOnlyMetadataStatistics GetMetadataStatistics();
    }
}