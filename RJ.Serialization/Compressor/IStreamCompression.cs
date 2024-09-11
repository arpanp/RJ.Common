using System;
using System.Collections.Generic;
using System.Text;

namespace RJ.Serialization
{
    public interface IStreamCompression
    {
        byte[] Compress(byte[] input);
        byte[] Decompress(byte[] input);
    }
}
