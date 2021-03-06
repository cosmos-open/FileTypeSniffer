﻿using Cosmos.FileTypeSniffers.Registering;

namespace Cosmos.FileTypeSniffers.Library
{
    // ReSharper disable once InconsistentNaming
    [FileTypeExtensionNames("jp2")]
    [FileTypeHex("00 00 00 0C 6A 50 20 20 0D 0A")]
    [FileTypeDescription("Various JPEG-2000 image file formats")]
    public class JP2_Class1 : IFileTypeRegistrar { }
}