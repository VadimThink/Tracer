using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace TracerLibrary
{
    public class Tracer : ITracer
    {
        private Dictionary<Thread, Stack<MethodTraceResult>> ProcessingQueries;

        private Dictionary<Thread, List<MethodTraceResult>> FinishedQueries;

        public Tracer()
        {
            ProcessingQueries = new Dictionary<Thread, Stack<MethodTraceResult>>();
            FinishedQueries = new Dictionary<Thread, List<MethodTraceResult>>();
        }


        public void StartTrace()
        {
            lock (ProcessingQueries)
            {
                var currentThread = Thread.CurrentThread;
                if (!ProcessingQueries.ContainsKey(currentThread))
                {
                    ProcessingQueries.Add(currentThread, new Stack<MethodTraceResult>());
                }
            }

            var stackTrace = new StackTrace();
            var callerMethod = stackTrace.GetFrame(1).GetMethod();
            var currentMethodTrace = new MethodTraceResult();

            currentMethodTrace._ClassType = callerMethod.ReflectedType.Name;
            currentMethodTrace._Name = callerMethod.Name;
            currentMethodTrace._Stopwatch = new Stopwatch();
            currentMethodTrace._Stopwatch.Start();
            
        }

        public void StopTrace()
        {
            lock (ProcessingQueries)
            {
                var currentThread = Thread.CurrentThread;
                Stack <MethodTraceResult> currentStack = ProcessingQueries[currentThread];
                MethodTraceResult currentMethodTrace = currentStack.Pop();
                currentMethodTrace._Stopwatch.Stop();
                if (currentStack.Count > 0)
                {
                    MethodTraceResult fatherTrace = currentStack.Peek();
                    if (fatherTrace._Methods == null)
                    {
                        fatherTrace._Methods = new List<MethodTraceResult>();
                    }
                    
                    fatherTrace._Methods.Add(currentMethodTrace);
                }
                else
                {
                    if (!FinishedQueries.ContainsKey(currentThread))
                    {
                        FinishedQueries.Add(currentThread, new List<MethodTraceResult>());
                    }
                    
                    FinishedQueries[currentThread].Add(currentMethodTrace);
                }
            }
        }

        public TraceResult GetTraceResult()
        {
            return new TraceResult(FinishedQueries);
        }
    }
}