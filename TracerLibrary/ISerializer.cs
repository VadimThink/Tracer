using System;
using System.IO;

namespace TracerLibrary
{
    public interface ISerializer
    {
        void Serialize(Object o, StreamWriter streamWriter);
    }
}