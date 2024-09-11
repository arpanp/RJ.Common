﻿namespace RJ.Serialization
{
    public interface ISerializer
    {
        string Serialize<T>(T obj);
        T Deserialize<T>(string input);
    }
}
