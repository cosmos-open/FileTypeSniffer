﻿using Cosmos.FileTypeSniffers.Registering;

namespace Cosmos.FileTypeSniffers.Library
{
    // ReSharper disable once InconsistentNaming
    [FileTypeExtensionNames("mpg mpeg")]
    [FileTypeHex("00 00 01 BA")]
    public class MPEG_Class2 : IFileTypeRegistrar { }
}
