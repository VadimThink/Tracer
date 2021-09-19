using System.IO;
using System.Xml.Serialization;

namespace TracerLibrary
{
    public class XmlSerializerAdapter : ISerializer
    {
        public void Serialize(object o, StreamWriter streamWriter)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(o.GetType());
            xmlSerializer.Serialize(streamWriter, o);
            streamWriter.WriteLine("\n");
            streamWriter.Flush();
        }
    }
}