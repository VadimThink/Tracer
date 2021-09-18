using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;

namespace TracerLibrary
{
    public class ThreadInfo
    {
        [XmlAttribute]
        private int Id { get; set; }
        [XmlAttribute]
        private string Time { get; set; }

        [XmlElement("method")]
        private List<MethodInfo> Methods { get; }

        public ThreadInfo()
        {
        }

        public ThreadInfo(Thread thread, List<MethodTraceResult> methodsResult)
        {
            Id = thread.ManagedThreadId;
            Methods = new List<MethodInfo>();
            long time = 0;
            foreach (var methodResult in methodsResult)
            {
                time += methodResult._Stopwatch.ElapsedMilliseconds;
                Methods.Add(new MethodInfo(methodResult));
            }

            this.Time = time + "ms";
        }

        public void AddMethod(MethodInfo method)
        {
            Methods.Add(method);
        }
    }
}