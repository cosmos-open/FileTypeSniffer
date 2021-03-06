using Cosmos.FileTypeSniffers.Registering;

namespace Cosmos.FileTypeSniffers.Library
{
    // ReSharper disable once InconsistentNaming
    [FileTypeExtensionNames("odp")]
    [FileTypeHex("50,4b,03,04")]
    [FileTypeDescription("OpenDocument Presentation")]
    public class ODP_Class1 : IFileTypeRegistrar { }
}