﻿using Cosmos.FileTypeSniffers.Registering;

namespace Cosmos.FileTypeSniffers.Library
{
    // ReSharper disable once InconsistentNaming
    [FileTypeExtensionNames("m2p,mpg2")]
    [FileTypeHex("00 00 01 BA")]
    public class M2P_Class1 : IFileTypeRegistrar { }
}