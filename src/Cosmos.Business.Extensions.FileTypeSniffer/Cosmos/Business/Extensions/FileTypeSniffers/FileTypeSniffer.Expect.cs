﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Business.Extensions.FileTypeSniffers.Core;
using Cosmos.Business.Extensions.FileTypeSniffers.Core.Extensions;

namespace Cosmos.Business.Extensions.FileTypeSniffers
{
    public class ExpectFileTypeSniffer : IFileTypeSniffer
    {
        private readonly IFileTypeSniffer _innerSniffer;
        private readonly List<string> _expectedResults;
        private readonly int _expectedCount;
        private readonly bool _singleMode;

        public ExpectFileTypeSniffer(IFileTypeSniffer instance, List<string> expectedResults) : this(instance, expectedResults, false) { }

        public ExpectFileTypeSniffer(IFileTypeSniffer instance, string expectedResult) : this(instance, new List<string> { expectedResult }, true) { }

        private ExpectFileTypeSniffer(IFileTypeSniffer instance, List<string> expectedResults, bool singleMode)
        {
            _innerSniffer = instance ?? throw new ArgumentNullException(nameof(instance));
            _expectedResults = FixedExpectedResults(expectedResults ?? throw new ArgumentNullException(nameof(expectedResults))).ToList();
            _expectedCount = expectedResults.Count;
            _singleMode = singleMode;
        }

        public List<string> Match(byte[] data, bool matchAll = false)
        {
            var rawResults = _innerSniffer.Match(data, matchAll);

            return _expectedCount == 0
                ? rawResults
                : Filter(rawResults, _expectedResults).ToList();
        }

        public List<string> Match(string filePath, int simpleLength, bool matchAll = false)
        {
            var rawResults = _innerSniffer.Match(filePath, simpleLength, matchAll);

            return _expectedCount == 0
                ? rawResults
                : Filter(rawResults, _expectedResults).ToList();
        }

        public string MatchSingle(byte[] data)
        {
            var rawResults = _innerSniffer.Match(data, true);

            if (_singleMode)
            {
                return rawResults.Any()
                    ? SingleFilter(rawResults, _expectedResults)
                    : string.Empty;
            }
            else
            {
                return _expectedCount == 0
                    ? rawResults.FirstOrDefault()
                    : Filter(rawResults, _expectedResults).FirstOrDefault();
            }
        }

        public string MatchSingle(string filePath, int simpleLength)
        {
            var rawResults = _innerSniffer.Match(filePath, simpleLength, true);

            if (_singleMode)
            {
                return rawResults.Any()
                    ? SingleFilter(rawResults, _expectedResults)
                    : string.Empty;
            }
            else
            {
                return _expectedCount == 0
                    ? rawResults.FirstOrDefault()
                    : Filter(rawResults, _expectedResults).FirstOrDefault();
            }
        }

        private static IEnumerable<string> Filter(List<string> rawResults, List<string> expectedResults)
        {
            foreach (var result in rawResults)
            {
                if (expectedResults.Contains(result))
                    yield return result;
            }
        }

        private static string SingleFilter(List<string> rawResults, List<string> expectedResults)
        {
            var expectedResult = expectedResults.Single();
            foreach (var result in rawResults)
            {
                if (string.Compare(result, expectedResult, StringComparison.OrdinalIgnoreCase) == 0)
                    return expectedResult;
            }

            return string.Empty;
        }

        private static IEnumerable<string> FixedExpectedResults(List<string> originalExpectedResults)
        {
            if (originalExpectedResults == null)
                throw new ArgumentNullException(nameof(originalExpectedResults));

            foreach (var result in originalExpectedResults)
            {
                if (string.IsNullOrWhiteSpace(result))
                    continue;
                yield return result.RemoveDotAtFirstPosition().ToLower();
            }
        }

        public IFileTypeSniffer Expect(List<string> expectedResults) => new ExpectFileTypeSniffer(_innerSniffer, expectedResults);

        public IFileTypeSniffer Expect(string expectedResult) => new ExpectFileTypeSniffer(_innerSniffer, expectedResult);

        public SniffingReadOnlyMetadataStatistics GetMetadataStatistics() => _innerSniffer.GetMetadataStatistics();
    }
}