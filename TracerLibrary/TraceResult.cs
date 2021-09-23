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
        private List<ThreadInfo> _threads;

        public TraceResult()
        { }

        public List<ThreadInfo> Threads
        {
            get => _threads;
            set => _threads = value;
        }

        public TraceResult(Dictionary<Thread, List<MethodTraceResult>> value)
        {
            _threads = new List<ThreadInfo>();
            foreach (var thread in value)
            {
                ThreadInfo threadInfo = new ThreadInfo(thread.Key, thread.Value);
                _threads.Add(threadInfo);
            }
        }
        
    }
}
