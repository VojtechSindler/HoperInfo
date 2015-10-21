using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_kraj
{
    class PCU
    {
        string name;

        public string Name
        {
            get { return name; }
        }
        string NumberOfCores;

        public string NumberOfCores1
        {
            get { return NumberOfCores; }
        }
        string NumberOfLogicalProcessors;

        public string NumberOfLogicalProcessors1
        {
            get { return NumberOfLogicalProcessors; }
        }

        string maxFreq;

        public string MaxFreq
        {
            get { return maxFreq; }
        }

        public PCU(string name, string NumberOfCores, string NumberOfLogicalProcessors, string MaxClockSpeed)
        {
            this.name = name;
            this.NumberOfCores = NumberOfCores;
            this.NumberOfLogicalProcessors = NumberOfLogicalProcessors;
            this.maxFreq = MaxClockSpeed;
        }
    }
}
