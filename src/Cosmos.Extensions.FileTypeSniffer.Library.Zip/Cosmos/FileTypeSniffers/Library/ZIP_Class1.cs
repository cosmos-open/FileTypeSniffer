using Cosmos.FileTypeSniffers.Registering;

namespace Cosmos.FileTypeSniffers.Library
{
    // ReSharper disable once InconsistentNaming
    [FileTypeExtensionNames("zip")]
    [FileTypeHex("50,4b,03,04")]
    public class ZIP_Class1 : IFileTypeRegistrar { }
}