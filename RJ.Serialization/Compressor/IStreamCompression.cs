namespace RJ.Serialization
{
    public interface IStreamCompression
    {
        byte[] Compress(byte[] input);
        byte[] Decompress(byte[] input);
    }
}
