using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;

namespace Main_Console
{
    class Assembly_Threads
    {
        Assembly _asm;
        List<Thread> _threads = new List<Thread>();

        public Assembly_Threads(Assembly asm)
        {
            _asm = asm;
        }

        public void Add(Thread t)
        {
            _threads.Add(t);
        }

        public Assembly asm
        {
            get
            {
                return _asm;
            }
            set
            {
                _asm = value;
            }
        }

        public List<Thread> threads
        {
            get
            { 
                return _threads; 
            }
            set
            {
                _threads = value;
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Assembly_Threads> ATs = new List<Assembly_Threads>();
            try
            {
                foreach (Assembly asm in EnumPluginFiles(@"E:\My Documents\Programming\Projects\Platform_Plugins\Plugins"))
                {
                    Assembly_Threads at = new Assembly_Threads(asm);
                    Thread curT = LoadDLL(asm, null);
                    at.Add(curT);
                    ATs.Add(at);
                    curT.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Press Enter to stop.");
                Console.Read();
                foreach (Assembly_Threads at in ATs)
                {
                    UnloadDLL(at);
                }
                Console.WriteLine("END. Press Enter to exit.");
                Console.ReadLine();
            }
        }

        static Thread LoadDLL(Assembly asm, object[] param)
        {
            Type[] ts = asm.GetExportedTypes();
            foreach (Type t in ts)
            {
                /// Deprecated, because this will call ToString method also, which leads to the exception.
                /// 
                //foreach (MethodInfo minfo in t.GetMethods())
                //{
                //    minfo.Invoke(null, param);
                //}
                return new Thread(new ThreadStart(
                    delegate
                    {
                        t.GetMethod("DOSSTONED_BG_OnStart").Invoke(null, param); 
                    }
                ));

            }
            throw new DllNotFoundException("The entry point of current DLL cannot be found.");
        }

        static void UnloadDLL(Assembly_Threads at)
        {
            Type[] ts = at.asm.GetExportedTypes();
            foreach (Type t in ts)
            {
                /// Deprecated, because this will call ToString method also, which leads to the exception.
                /// 
                //foreach (MethodInfo minfo in t.GetMethods())
                //{
                //    minfo.Invoke(null, param);
                //}
                if (t.GetMethod("DOSSTONED_BG_OnStop") == null)
                    throw new DllNotFoundException("The exit point of current DLL cannot be found.");
                Thread th = new Thread(new ThreadStart(
                    delegate
                    {
                        t.GetMethod("DOSSTONED_BG_OnStop").Invoke(null, null);
                    }
                ));
                th.Start();
                at.threads.Add(th);
                break;

            }

            /// wait for 1 second to wait other to terminate themselves.
            /// 
            Thread.Sleep(1000);


            foreach (Thread t in at.threads)
            {
                if (t != null)
                    if (t.IsAlive)
                        t.Abort();
            }
            
        }

        static Assembly[] EnumPluginFiles(string dir_path)
        {
            string[] files = System.IO.Directory.GetFiles(dir_path);
            Assembly[] ret = new Assembly[files.Length];
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = Assembly.LoadFile(files[i]);
            }
            return ret;
        }
    }
}
