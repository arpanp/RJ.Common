﻿namespace RJ.Serialization
{
    public sealed class DummyCompression : IStreamCompression
    {
        public byte[] Compress(byte[] input)
        {
            return input;
        }

        public byte[] Decompress(byte[] input)
        {
            return input;
        }
    }
}
