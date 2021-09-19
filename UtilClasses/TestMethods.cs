using System;
using System.Threading;
using TracerLibrary;

namespace UtilClasses
{
    public class TestMethods
    {
        private readonly ITracer _tracer;

        public TestMethods(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void FastestMethod()
        {
            _tracer.StartTrace();
            Console.WriteLine("Fastest method");
            _tracer.StopTrace();
        }

        public void NormalMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(50);
            Console.WriteLine("Normal method");
            _tracer.StopTrace();
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
            _tracer.StopTrace();
        }
    }
}