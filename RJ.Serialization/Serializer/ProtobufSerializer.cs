using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RJ.Serialization
{
    public sealed class ProtobufSerializer : ISerializer
    {
        private readonly IStreamCompression _streamCompression;

        public ProtobufSerializer(IStreamCompression streamCompression)
        {
            _streamCompression = streamCompression;
        }

        public T Deserialize<T>(string input)
        {
            byte[] arr = Convert.FromBase64String(input);
            var decompressed = _streamCompression.Decompress(arr);
            using (MemoryStream ms = new MemoryStream(decompressed))
                return Serializer.Deserialize<T>(ms);
        }

        public string Serialize<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, obj);
                var compressed = _streamCompression.Compress(ms.ToArray());
                return Convert.ToBase64String(compressed);
            }
        }
    }
}
