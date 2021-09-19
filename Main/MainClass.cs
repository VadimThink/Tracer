using System;
using System.Runtime.InteropServices;
using System.Threading;
using SerializerLibrary;
using TracerLibrary;
using UtilClasses;

namespace Main
{
    class MainClass
    {
        private static ITracer _tracer;

        static void Main(string[] args)
        {
            _tracer = new Tracer();
            TestMethods testMethods = new TestMethods(_tracer);
            var fastestThread = new Thread(testMethods.FastestMethod);
            var normalThread = new Thread(testMethods.NormalMethod);
            var slowThread = new Thread(testMethods.SlowMethod);
            var slowestThread = new Thread(testMethods.SlowestMethod);
            fastestThread.Start();
            normalThread.Start();
            slowThread.Start();
            slowestThread.Start();

            Printer printer = new Printer();
            printer.AddSerializer(new JsonSerializerAdapter());
            printer.AddSerializer(new XmlSerializerAdapter());
            printer.AddStream(Console.OpenStandardOutput());
            printer.AddStreamToFile("output.txt");
            printer.Print(_tracer.GetTraceResult());
            Console.ReadKey();
        }

    }
}
