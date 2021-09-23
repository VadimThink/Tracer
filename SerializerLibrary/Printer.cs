using System.Collections.Generic;
using System.IO;

namespace SerializerLibrary
{
    public class Printer
    {
        private List<ISerializer> _serializers;
        private List<StreamWriter> _streamWriters;

        public Printer()
        {
            _serializers = new List<ISerializer>();
            _streamWriters = new List<StreamWriter>();
        }

        public void AddStream(Stream stream)
        {
            _streamWriters.Add(new StreamWriter(stream));
        }

        public void AddStreamToFile(string fileName)
        {
            FileStream fileStream = File.Create(fileName);
            _streamWriters.Add(new StreamWriter(fileStream));
        }

        public void AddSerializer(ISerializer serializer)
        {
            _serializers.Add(serializer);
        }

        public void Print(object data)
        {
            foreach (var serializer in _serializers)
            {
                foreach (var streamWriter in _streamWriters)
                {
                    serializer.Serialize(data, streamWriter);
                }
            }
        }
        
    }
}