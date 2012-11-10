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
            var type = typeof(ITesting);
            var types = AppDomain.CurrentDomain.GetAssemblies().ToList().SelectMany(s => s.GetTypes()).Where(p => type.IsAssignableFrom(p));
            foreach (Type t in types)
            {
                if (!t.IsInterface)
                {
                    object o = Activator.CreateInstance(t);
                    MethodInfo info = t.GetMethod("DoTest");
                    object[] parameters = new object[] { null, null, null };
                    Boolean result = (Boolean)info.Invoke(o, parameters);

                    if (result)
                    {
                        int pass = (int) parameters[0];
                        int fail = (int) parameters[1];
                        List<string> messages = (List<string>) parameters[2];

                        Console.WriteLine(string.Format("[{0}] {0} tests passed", t.ToString(), pass));
                        Console.WriteLine(string.Format("[{0}] {0} non-fatal errors", t.ToString(), fail));
                        foreach (string s in messages)
                            Console.WriteLine(string.Format("[{0}] {1}", t.ToString(), s));
                    }
                    else
                    {
                        Console.WriteLine(string.Format("[{0}] A fatal error has occured", t.ToString()));
                    }
                }
            }

            Console.WriteLine("==================================================\n");

            return 0;
        }
    }
}