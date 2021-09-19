using System;
using System.Threading;
using TracerLibrary;

namespace UtilClasses
{
    public class TestMethodsFast
    {
        private ITracer _tracer;

        public TestMethodsFast(ITracer tracer)
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
            Console.WriteLine("Normal method");
            Thread.Sleep(50);
            FastestMethod();
            _tracer.StopTrace();
        }
        
    }
}