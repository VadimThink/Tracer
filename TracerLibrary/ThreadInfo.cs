using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Xml.Serialization;

namespace TracerLibrary
{
    public class ThreadInfo
    {
        [XmlAttribute]
        public int Id { get; set; }
        [XmlAttribute]
        public string Time { get; set; }

        [XmlElement("method")]
        public List<MethodInfo> methods { get; }

        public ThreadInfo()
        {
        }

        public ThreadInfo(Thread thread, List<MethodTraceResult> methodsResult)
        {
            Id = thread.ManagedThreadId;
            methods = new List<MethodInfo>();
            long time = 0;
            foreach (var methodResult in methodsResult)
            {
                time += methodResult.Stopwatch.ElapsedMilliseconds;
                methods.Add(new MethodInfo(methodResult));
            }

            this.Time = time + "ms";
        }

        public void AddMethod(MethodInfo method)
        {
            methods.Add(method);
        }
    }
}