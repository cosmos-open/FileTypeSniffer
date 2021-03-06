﻿using Cosmos.FileTypeSniffers.Registering;

namespace Cosmos.FileTypeSniffers.Library
{
    // ReSharper disable once InconsistentNaming
    [FileTypeExtensionNames("fits")]
    [FileTypeHex("3D 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 54")]
    [FileTypeDescription("Flexible Image Transport System")]
    public class FITS_Class1 : IFileTypeRegistrar { }
}