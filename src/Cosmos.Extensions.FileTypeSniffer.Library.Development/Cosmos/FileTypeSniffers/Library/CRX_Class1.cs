﻿using Cosmos.FileTypeSniffers.Registering;

namespace Cosmos.FileTypeSniffers.Library
{
    // ReSharper disable once InconsistentNaming
    [FileTypeExtensionNames("crx")]
    [FileTypeHex("43 72 32 34")]
    [FileTypeDescription("Google Chrome extensions file")]
    public class CRX_Class1 : IFileTypeRegistrar { }
}