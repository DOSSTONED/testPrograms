using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System;

namespace DOSSTONED_BG_V0._2
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

    /// <summary>
    /// This is the class to handle dlls
    /// </summary>
    static class DLLReg
    {
        /// <summary>
        /// Register the DLL
        /// </summary>
        /// <param name="asm">The assemble file</param>
        /// <param name="param">The parameters to pass to entry function DOSSTONED_BG_OnStart</param>
        /// <returns>The thread handles the dll.</returns>
        public static Thread LoadDLL(Assembly asm, object[] param)
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
                Thread ret = new Thread(new ThreadStart(
                    delegate
                    {
                        MethodInfo mi = t.GetMethod("DOSSTONED_BG_OnStart");
                        mi.Invoke(null, param);
                    }
                ));

                return ret;
                
            }
            
            //throw new DllNotFoundException("The entry point of current DLL cannot be found.");
            System.IO.File.AppendAllText(@"E:\TEMP\DOSSTONED_BG.txt", "The entry point of current DLL cannot be found.\r\n");
            return null;
        }

        /// <summary>
        /// Unregister a dll
        /// </summary>
        /// <param name="at">The class of Assembly_Threads</param>
        public static void UnloadDLL(Assembly_Threads at)
        {
            Console.WriteLine("Test.");
            Type[] ts = at.asm.GetExportedTypes();
            bool needToCallStop = false;
            foreach (Thread t in at.threads)
            {
                if (t != null)
                    if (t.IsAlive)
                    {
                        needToCallStop = true;
                        break;
                    }
            }
            if (needToCallStop)
            {
                foreach (Type t in ts)
                {
                    /// Deprecated, because this will call ToString method also, which leads to the exception.
                    /// 
                    //foreach (MethodInfo minfo in t.GetMethods())
                    //{
                    //    minfo.Invoke(null, param);
                    //}
                    if (t.GetMethod("DOSSTONED_BG_OnStop") == null)
                        System.IO.File.AppendAllText(@"E:\TEMP\DOSSTONED_BG.txt", "The exit point of current DLL cannot be found.");
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

        /// <summary>
        /// Returns the assembles in the specific directory
        /// </summary>
        /// <param name="dir_path">A directory contains plug-ins</param>
        /// <returns>An array of assembles</returns>
        public static Assembly[] EnumPluginFiles(string dir_path)
        {

            string[] files = System.IO.Directory.GetFiles(dir_path, "*.dll");
            Assembly[] ret = new Assembly[files.Length];
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = Assembly.LoadFile(files[i]);
            }
            return ret;
        }
    }
}
