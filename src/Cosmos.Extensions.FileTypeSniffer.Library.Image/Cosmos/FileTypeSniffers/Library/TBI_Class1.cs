﻿using Cosmos.FileTypeSniffers.Registering;

namespace Cosmos.FileTypeSniffers.Library
{
    // ReSharper disable once InconsistentNaming
    [FileTypeExtensionNames("tbi")]
    [FileTypeHex("00 00 00 00 14 00 00 00")]
    public class TBI_Class1 : IFileTypeRegistrar { }
}