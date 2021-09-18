using System.IO;
using Newtonsoft.Json;

namespace TracerLibrary
{
    public class JsonSerializerAdapter : ISerializer
    {
        public void Serialize(object o, StreamWriter stream)
        {
            var serializer = new JsonSerializer()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };

            serializer.Serialize(stream, o);
            stream.WriteLine("\n");
            stream.Flush();
        }
    }
}