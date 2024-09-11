# Serialization.Common

This common Serialization provide logic regarding serialization and deserialization in single library using different serializers like Protobuf, Newtonsoft.Json serialization, Binary Formatter.

For serialized data compression, added GZipCompression. If developer do not need to use it, they can use DummyCompression.

Demo Code

IStreamCompression streamCompression = new RJ.Serialization.GZipCompression();
ISerializer serializer = new ProtobufSerializer(streamCompression);
