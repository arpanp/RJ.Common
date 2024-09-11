using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace RJ.Serialization
{
    public sealed class BinarySerializer : ISerializer
    {
        private readonly IStreamCompression _streamCompression;
        private readonly BinaryFormatter formatter;

        public BinarySerializer(IStreamCompression streamCompression)
        {
            _streamCompression = streamCompression;
            formatter = new BinaryFormatter();
        }

        public T Deserialize<T>(string input)
        {
            byte[] arr = Convert.FromBase64String(input);
            var decompressed = _streamCompression.Decompress(arr);
            using (var ms = new MemoryStream(decompressed))
            {
                return (T)formatter.Deserialize(ms);
            }
        }

        public string Serialize<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, obj);
                var compressed = _streamCompression.Compress(ms.ToArray());
                return Convert.ToBase64String(compressed);
            }
        }
    }
}
