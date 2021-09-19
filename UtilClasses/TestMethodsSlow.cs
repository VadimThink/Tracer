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
            Thread.Sleep(100);
            Console.WriteLine("Slow method");
            _tracer.StopTrace();
        }

        public void SlowestMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(150);
            Console.WriteLine("Slowest method");
            SlowMethod();
            _tracer.StopTrace();
        }
    }
}