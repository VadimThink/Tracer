using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;

namespace TracerLibrary
{
    public class ThreadInfo
    {
        [XmlAttribute] 
        private int _id;

        [XmlAttribute] 
        private string _time;

        [XmlElement("method")] 
        private List<MethodInfo> _methods;
        
        public ThreadInfo()
        {
        }

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Time
        {
            get => _time;
            set => _time = value;
        }

        public List<MethodInfo> Methods
        {
            get => _methods;
            set => _methods = value;
        }

        public ThreadInfo(Thread thread, List<MethodTraceResult> methodsResult)
        {
            _id = thread.ManagedThreadId;
            _methods = new List<MethodInfo>();
            long time = 0;
            foreach (var methodResult in methodsResult)
            {
                time += methodResult.Stopwatch.ElapsedMilliseconds;
                _methods.Add(new MethodInfo(methodResult));
            }

            _time = time + "ms";
        }

        public void AddMethod(MethodInfo method)
        {
            _methods.Add(method);
        }
    }
}