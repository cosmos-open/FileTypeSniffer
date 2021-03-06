﻿using System.Text;
using Cosmos.FileTypeSniffers.Core.Extensions;

namespace Cosmos.FileTypeSniffers.Core
{
    public class SniffingOffset
    {
        private SniffingOffset() { }

        public int Length { get; private set; }

        public int Start { get; private set; }

        public string Value { get; private set; }

        public static SniffingOffset Create(string[] byteStringArray, int start, int length)
        {
            return new SniffingOffset
            {
                Start = start,
                Length = length,
                Value = Encoding.ASCII.GetString(string.Join(",", byteStringArray, start, length).GetByte())
            };
        }
    }
}
