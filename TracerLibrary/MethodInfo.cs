using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace TracerLibrary
{
    public class MethodInfo
    {
        [XmlAttribute]
        private string Name { get; set; }
        [XmlAttribute]
        private string Time { get; set; }
        [XmlAttribute("class")]
        [JsonProperty("class")]
        private string ClassName { get; set; }
        [XmlElement("method")]
        private List<MethodInfo> Methods { get; }

        public MethodInfo()
        {
        }

        public MethodInfo(MethodTraceResult methodTraceResult)
        {
            Name = methodTraceResult.Name;
            Time = methodTraceResult.Stopwatch.ElapsedMilliseconds + "ms";
            ClassName = methodTraceResult.ClassType;
            Methods = new List<MethodInfo>();
            if (methodTraceResult.Methods == null) return;
            foreach (var childTraceResult in methodTraceResult.Methods)
            {
                Methods.Add(new MethodInfo(childTraceResult));
            }
        }

    }
}