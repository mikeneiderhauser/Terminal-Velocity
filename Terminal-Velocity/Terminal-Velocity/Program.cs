using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;
using Utility;

namespace TerminalVelocity
{
    public class Program
    {
        static void Main(string[] args)
        {
            Environment e = new Environment();
            e.Tick += e_Tick;

            while (true) ;
        }

        static void e_Tick(object sender, TickEventArgs e)
        {
            Console.WriteLine("Ticks " + e.Ticks);
        }
    }
}
