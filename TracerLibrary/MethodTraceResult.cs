using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TracerLibrary
{
    [Serializable]
    public class MethodTraceResult
    {
        private Stopwatch Stopwatch;
        private string Name;
        private string ClassType;
        private List<MethodTraceResult> Methods;

        public MethodTraceResult()
        {}
        public MethodTraceResult(Stopwatch stopwatch, string name, string classType, List<MethodTraceResult> methods)
        {
            Stopwatch = stopwatch;
            Name = name;
            ClassType = classType;
            Methods = methods;
        }

        public Stopwatch _Stopwatch
        {
            get => Stopwatch;
            set => Stopwatch = value;
        }

        public string _Name
        {
            get => Name;
            set => Name = value;
        }

        public string _ClassType
        {
            get => ClassType;
            set => ClassType = value;
        }

        public List<MethodTraceResult> _Methods
        {
            get => Methods;
            set => Methods = value;
        }
    }
}