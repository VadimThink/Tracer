using System.Collections.Generic;
using System.IO;

namespace SerializerLibrary
{
    public class Printer
    {
        private List<ISerializer> Serializers;
        private List<StreamWriter> StreamWriters;

        public Printer()
        {
            Serializers = new List<ISerializer>();
            StreamWriters = new List<StreamWriter>();
        }

        public void AddStream(Stream stream)
        {
            StreamWriters.Add(new StreamWriter(stream));
        }

        public void AddStreamToFile(string fileName)
        {
            FileStream fileStream = File.Create(fileName);
            StreamWriters.Add(new StreamWriter(fileStream));
        }

        public void AddSerializer(ISerializer serializer)
        {
            Serializers.Add(serializer);
        }

        public void Print(object data)
        {
            foreach (var serializer in Serializers)
            {
                foreach (var streamWriter in StreamWriters)
                {
                    serializer.Serialize(data, streamWriter);
                }
            }
        }
        
    }
}