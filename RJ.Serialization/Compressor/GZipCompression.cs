using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Text;

namespace RJ.Serialization
{
    public sealed class GZipCompression : IStreamCompression
    {
        public byte[] Compress(byte[] input)
        {
            using (var source = new MemoryStream(input))
            {
                using (var result = new MemoryStream())
                {
                    using (var Compress = new GZipStream(result, CompressionMode.Compress))
                    {
                        source.CopyTo(Compress);
                    }

                    return result.ToArray();
                }
            }
        }

        public byte[] Decompress(byte[] input)
        {
            using (var source = new MemoryStream(input))
            {
                using (var result = new MemoryStream())
                {
                    using (var Decompress = new GZipStream(source, CompressionMode.Decompress))
                    {
                        Decompress.CopyTo(result);
                    }
                    return result.ToArray();
                }
            }
        }
    }
}
