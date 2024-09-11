using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace RJ.Serialization
{
    public sealed class JsonSerializer : ISerializer
    {
        private readonly IStreamCompression _streamCompression;
        private readonly Newtonsoft.Json.JsonSerializer serializer;

        public JsonSerializer(IStreamCompression streamCompression)
        {
            _streamCompression = streamCompression;
            serializer = new Newtonsoft.Json.JsonSerializer();
        }

        public T Deserialize<T>(string input)
        {
            byte[] arr = Convert.FromBase64String(input);
            var decompressed = _streamCompression.Decompress(arr);
            using (var ms = new MemoryStream(decompressed))
            {
                using (var streamReader = new StreamReader(ms))
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    return serializer.Deserialize<T>(jsonReader);
                }
            }
        }

        public string Serialize<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(ms))
                using (var jsonWriter = new JsonTextWriter(streamWriter))
                {
                    serializer.Serialize(jsonWriter, obj);
                    streamWriter.Flush();
                    ms.Seek(0, SeekOrigin.Begin);
                    var compressed = _streamCompression.Compress(ms.ToArray());
                    return Convert.ToBase64String(compressed);
                }
            }
        }
    }
}
