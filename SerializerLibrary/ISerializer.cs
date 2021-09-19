using System;
using System.IO;

namespace SerializerLibrary
{
    public interface ISerializer
    {
        void Serialize(Object o, StreamWriter streamWriter);
    }
}