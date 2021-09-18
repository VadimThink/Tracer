using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TracerLibrary
{
    [Serializable]
    public class MethodTraceResult
    {
        public Stopwatch Stopwatch;
        public string Name;
        public string ClassType;
        public List<MethodTraceResult> Methods; 
    }
}