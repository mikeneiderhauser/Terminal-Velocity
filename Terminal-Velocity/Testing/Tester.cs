using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interfaces;

namespace Testing
{
    public class Tester
    {
            static int Main(String[] args)
            {
                int pass = 0;
                int fail = 0;

                var type = typeof(ITesting);
                var types = AppDomain.CurrentDomain.GetAssemblies().ToList().SelectMany(s => s.GetTypes()).Where(p => type.IsAssignableFrom(p));
                foreach (Type t in types)
                {
                    if (!t.IsInterface)
                    {
                        object o = Activator.CreateInstance(t);
                        MethodInfo info = t.GetMethod("DoTest");
                        Boolean result = (Boolean) info.Invoke(o, new object[] { });

                        if (result)
                        {
                            pass++;
                            Console.WriteLine(string.Format("[Test] success: {0}", t.ToString()));
                        }
                        else
                        {
                            fail++;
                            Console.WriteLine(string.Format("[Test] FAILED: {0}", t.ToString()));
                        }
                    }
                }

                Console.WriteLine("\n\n");
                Console.WriteLine(string.Format("All tests completed: {0} out of {1} tests passed!", pass, pass + fail));
                Console.WriteLine("=========================================================\nPress enter to continue...");
                Console.ReadLine();

                return 0;
            }
        }
    }
}
