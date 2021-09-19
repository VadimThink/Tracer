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

        public static void Main(string[] args)
        {
            _tracer = new Tracer();
            var thread = new Thread(StartNormalMethod);
            thread.Start();
            StartSlowestMethod();
            thread.Join();

            Printer printer = new Printer();
            printer.AddSerializer(new JsonSerializerAdapter());
            printer.AddSerializer(new XmlSerializerAdapter());
            printer.AddStream(Console.OpenStandardOutput());
            printer.AddStreamToFile("output.txt");
            printer.Print(_tracer.GetTraceResult());
            Console.ReadKey();
        }

        public static void StartNormalMethod()
        {
            TestMethodsFast fast = new TestMethodsFast(_tracer);
            fast.NormalMethod();
        }

        public static void StartSlowestMethod()
        {
            TestMethodsSlow slow = new TestMethodsSlow(_tracer);
            slow.SlowestMethod();
        }

    }
}
