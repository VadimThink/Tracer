using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using TracerLibrary;
using UtilClasses;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ITracer tracer = new Tracer();
            var slow = new TestMethodsSlow(tracer);
            slow.SlowestMethod();
            var traceResult = tracer.GetTraceResult();
            Assert.IsNotNull(traceResult);
            Assert.AreEqual(1, traceResult.Threads.Count,
                string.Format("Expected {0} threads, instead got {1}", 1, traceResult.Threads.Count));
        }

        [TestMethod]
        public void TestMethod2()
        {
            ITracer tracer = new Tracer();
            var thread = new Thread(StartFastMethods);
            thread.Start(tracer);
            StartSuperSlowMethods(tracer);
            thread.Join();


            var traceResult = tracer.GetTraceResult();
            Assert.AreEqual(2, traceResult.Threads.Count,
                string.Format("Expected {0} threads, instead got {1}", 2, traceResult.Threads.Count));
        }

        [TestMethod]
        public void TestMethod3()
        {
            ITracer tracer = new Tracer();
            StartFastMethods(tracer);
            StartSuperSlowMethods(tracer);
            StartFastMethods(tracer);
            StartSuperSlowMethods(tracer);

            var traceResult = tracer.GetTraceResult();
            Assert.AreEqual(4, traceResult.Threads[0].Methods.Count,
                string.Format("Expected {0} root methods in main thread, instead got {1}", 4,
                    traceResult.Threads[0].Methods.Count));

        }

        [TestMethod]
        public void TestMethod4()
        {
            ITracer tracer = new Tracer();
            var thread1 = new Thread(StartSlowMethods);
            thread1.Start(tracer);
            StartSuperFastMethods(tracer);
            StartFastMethods(tracer);
            StartSuperSlowMethods(tracer);
            thread1.Join();

            var traceResult = tracer.GetTraceResult();

            int methodsAtAll = 0;
            foreach (var thread in traceResult.Threads)
            {
                foreach (var method in thread.Methods)
                {
                    methodsAtAll += 1 + countNestedMethods(method);
                }
            }

            Assert.AreEqual(6, methodsAtAll,
                string.Format("Expected {0} methods at all, instead got {1}", 6, methodsAtAll));
        }

        private int countNestedMethods(MethodInfo method)
        {
            int nestedMethodCount = 0;
            if (method.Methods != null)
            {
                foreach (var childMethod in method.Methods)
                {
                    nestedMethodCount += 1 + countNestedMethods(childMethod);
                }
            }

            return nestedMethodCount;
        }

        public static void StartFastMethods(object tracer)
        {
            TestMethodsFast fast = new TestMethodsFast(tracer as ITracer);
            fast.NormalMethod();
        }
        public static void StartSuperFastMethods(object tracer)
        {
            TestMethodsFast fast = new TestMethodsFast(tracer as ITracer);
            fast.FastestMethod();
        }

        public static void StartSuperSlowMethods(object tracer)
        {
            TestMethodsSlow slow = new TestMethodsSlow(tracer as ITracer);
            slow.SlowestMethod();
        }
        public static void StartSlowMethods(object tracer)
        {
            TestMethodsSlow slow = new TestMethodsSlow(tracer as ITracer);
            slow.SlowMethod();
        }
    }
}
