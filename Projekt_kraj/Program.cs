using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Diagnostics;



namespace Projekt_kraj
{
    class Program
    {
       
        static void Main(string[] args)
        {
            Console.WriteLine("Program verze 0.1 Prelease Version");
            System_information sys = new System_information();
            Save save = new Save();
            /*
             * chybi tiskarny a USBcka
             */
            Stopwatch ProgramRunning = new Stopwatch();
            ProgramRunning.Start();
            save.SaveXML();
            ProgramRunning.Stop();
            Console.WriteLine("Time elapsed: {0}",ProgramRunning.Elapsed);
            //sys.LastUpdateTime();

            Console.ReadLine();
        }
    }
}
