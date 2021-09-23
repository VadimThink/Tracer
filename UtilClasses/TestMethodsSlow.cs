using System;
using System.Threading;
using TracerLibrary;

namespace UtilClasses
{
    public class TestMethodsSlow
    {
        private ITracer _tracer;

        public TestMethodsSlow(ITracer tracer)
        {
            _tracer = tracer;
        }
        
        public void SlowMethod()
        {
            _tracer.StartTrace();
            Console.WriteLine("Slow method");
            Thread.Sleep(100);
            _tracer.StopTrace();
        }

        public void SlowestMethod()
        {
            _tracer.StartTrace();
            Console.WriteLine("Slowest method");
            Thread.Sleep(150);
            SlowMethod();
            _tracer.StopTrace();
        }
    }
}