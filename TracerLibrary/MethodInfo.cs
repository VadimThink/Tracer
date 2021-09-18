using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace TracerLibrary
{
    public class MethodInfo
    {
        [XmlAttribute]
        private string name { get; set; }
        [XmlAttribute]
        private string time { get; set; }
        [XmlAttribute("class")]
        [JsonProperty("class")]
        public string className { get; set; }
        [XmlElement("method")]
        public List<MethodInfo> methods { get; }

        public MethodInfo()
        {
        }

        public MethodInfo(MethodTraceResult methodTraceResult)
        {
            name = methodTraceResult.Name;
            time = methodTraceResult.Stopwatch.ElapsedMilliseconds + "ms";
            className = methodTraceResult.ClassType;
            methods = new List<MethodInfo>();
            if (methodTraceResult.Methods != null)
                foreach (var childTraceResult in methodTraceResult.Methods)
                {
                    methods.Add(new MethodInfo(childTraceResult));
                }
        }

    }
}