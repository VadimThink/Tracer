using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;

namespace TracerLibrary
{
    [XmlRoot]
    public class TraceResult
    {
        [XmlElement("thread")]
        private List<ThreadInfo> Threads { get; }

        public TraceResult()
        {
            
        }

        public TraceResult(Dictionary<Thread, List<MethodTraceResult>> value)
        {
            Threads = new List<ThreadInfo>();
            foreach (var thread in value)
            {
                ThreadInfo threadInfo = new ThreadInfo(thread.Key, thread.Value);
                Threads.Add(threadInfo);
            }
        }
        
    }
}
