using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Interfaces;

namespace Testing
{
    public class Tester
    {
        static int Main(String[] args)
        {
            int totalPass = 0;
            int totalFail = 0;
            int totalFatal = 0;

            var type = typeof(ITesting);
            var types = AppDomain.CurrentDomain.GetAssemblies().ToList().SelectMany(s => s.GetTypes()).Where(p => type.IsAssignableFrom(p));
            foreach (Type t in types)
            {
                if (!t.IsInterface)
                {
                    Console.WriteLine("==================================================");
                    Console.WriteLine(string.Format("[{0}]", t.ToString()));

                    object o = Activator.CreateInstance(t);
                    MethodInfo info = t.GetMethod("DoTest");
                    object[] parameters = new object[] { null, null, null };
                    Boolean result = (Boolean)info.Invoke(o, parameters);

                    if (result)
                    {
                        int pass = (int) parameters[0];
                        int fail = (int) parameters[1];
                        List<string> messages = (List<string>) parameters[2];

                        Console.WriteLine(string.Format("{0} tests passed", pass));
                        Console.WriteLine(string.Format("{0} non-fatal errors", fail));
                        foreach (string s in messages)
                            Console.WriteLine(string.Format("Message : {0}", s));

                        totalPass += pass;
                        totalFail += fail;
                    }
                    else
                    {
                        Console.WriteLine(string.Format("A fatal error has occured"));
                        totalFail++;
                    }

                    Console.WriteLine("\n");
                }
            }

            Console.WriteLine("==================================================");
            Console.WriteLine("==================================================");
            Console.WriteLine("Testing completed.\n");
            Console.WriteLine(string.Format("Total Passed : {0} \nTotal Fail : {1} \nTotal Fatal Errors : {2}", totalPass, totalFail, totalFatal));
            Console.WriteLine("==================================================");
            Console.WriteLine("==================================================");
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();

            return 0;
        }
    }
}