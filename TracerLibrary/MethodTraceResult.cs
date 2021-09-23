using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TracerLibrary
{
    [Serializable]
    public class MethodTraceResult
    {
        private Stopwatch _stopwatch;
        private string _name;
        private string _classType;
        private List<MethodTraceResult> _methods;

        public MethodTraceResult()
        {}
        public MethodTraceResult(Stopwatch stopwatch, string name, string classType, List<MethodTraceResult> methods)
        {
            _stopwatch = stopwatch;
            _name = name;
            _classType = classType;
            _methods = methods;
        }

        public Stopwatch Stopwatch
        {
            get => _stopwatch;
            set => _stopwatch = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string ClassType
        {
            get => _classType;
            set => _classType = value;
        }

        public List<MethodTraceResult> Methods
        {
            get => _methods;
            set => _methods = value;
        }
    }
}