using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace TracerLibrary
{
    public class Tracer : ITracer
    {
        private Dictionary<Thread, Stack<MethodTraceResult>> _processingQueries;

        private Dictionary<Thread, List<MethodTraceResult>> _finishedQueries;

        public Tracer()
        {
            _processingQueries = new Dictionary<Thread, Stack<MethodTraceResult>>();
            _finishedQueries = new Dictionary<Thread, List<MethodTraceResult>>();
        }


        public void StartTrace()
        {
            var currentThread = Thread.CurrentThread;
            lock (_processingQueries)
            {
                
                if (!_processingQueries.ContainsKey(currentThread))
                {
                    _processingQueries.Add(currentThread, new Stack<MethodTraceResult>());
                }
            }

            var stackTrace = new StackTrace();
            var callerMethod = stackTrace.GetFrame(1).GetMethod();
            var currentMethodTrace = new MethodTraceResult();

            currentMethodTrace.ClassType = callerMethod.ReflectedType.Name;
            currentMethodTrace.Name = callerMethod.Name;
            currentMethodTrace.Stopwatch = new Stopwatch();
            currentMethodTrace.Stopwatch.Start();
            _processingQueries[currentThread].Push(currentMethodTrace);
        }

        public void StopTrace()
        {
            lock (_processingQueries)
            {
                var currentThread = Thread.CurrentThread;
                Stack <MethodTraceResult> currentStack = _processingQueries[currentThread];
                MethodTraceResult currentMethodTrace = currentStack.Pop();
                currentMethodTrace.Stopwatch.Stop();
                if (currentStack.Count > 0)
                {
                    MethodTraceResult fatherTrace = currentStack.Peek();
                    if (fatherTrace.Methods == null)
                    {
                        fatherTrace.Methods = new List<MethodTraceResult>();
                    }
                    
                    fatherTrace.Methods.Add(currentMethodTrace);
                }
                else
                {
                    if (!_finishedQueries.ContainsKey(currentThread))
                    {
                        _finishedQueries.Add(currentThread, new List<MethodTraceResult>());
                    }
                    
                    _finishedQueries[currentThread].Add(currentMethodTrace);
                }
            }
        }

        public TraceResult GetTraceResult()
        {
            return new TraceResult(_finishedQueries);
        }
    }
}