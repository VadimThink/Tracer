using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace TracerLibrary
{
    public class MethodInfo
    {
        [XmlAttribute] 
        private string _name;
        [XmlAttribute] 
        private string _time;

        [XmlAttribute("class")] 
        [JsonProperty("class")]
        private string _className;

        [XmlElement("method")] 
        private List<MethodInfo> _methods;

        public MethodInfo()
        {
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Time
        {
            get => _time;
            set => _time = value;
        }

        public string ClassName
        {
            get => _className;
            set => _className = value;
        }

        public List<MethodInfo> Methods
        {
            get => _methods;
            set => _methods = value;
        }

        public MethodInfo(MethodTraceResult methodTraceResult)
        {
            _name = methodTraceResult.Name;
            _time = methodTraceResult.Stopwatch.ElapsedMilliseconds + "ms";
            _className = methodTraceResult.ClassType;
            _methods = new List<MethodInfo>();
            if (methodTraceResult.Methods == null) return;
            foreach (var childTraceResult in methodTraceResult.Methods)
            {
                _methods.Add(new MethodInfo(childTraceResult));
            }
        }

    }
}