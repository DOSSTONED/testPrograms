using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;

namespace DOSSTONED_FrontEnd_V0._1
{
    public partial class Form1 : Form
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
                if (t.GetMethod("DOSSTONED_FE_LOAD", Type.GetTypeArray(param)) == null)
                    throw new BadImageFormatException("The entry point of current DLL cannot be found.");

                return new Thread(new ThreadStart(
                    delegate
                    {
                        t.GetMethod("DOSSTONED_FE_LOAD", Type.GetTypeArray(param)).Invoke(null, param);
                    }
                ));

            }
            throw new DllNotFoundException("The entry point of current assemble cannot be found.");
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
                if (t.GetMethod("DOSSTONED_FE_UNLOAD") == null)
                    throw new BadImageFormatException("The exit point of current DLL cannot be found.");
                Thread th = new Thread(new ThreadStart(
                    delegate
                    {
                        t.GetMethod("DOSSTONED_FE_UNLOAD").Invoke(null, null);
                    }
                ));
                th.Start();
                at.threads.Add(th);
                break;

            }

            /// wait for 0.1 second to wait other to terminate themselves.
            /// 
            Thread.Sleep(100);


            foreach (Thread t in at.threads)
            {
                if (t != null)
                    if (t.IsAlive)
                        t.Abort();
            }

        }

        static List<Assembly> EnumPluginFiles(string dir_path)
        {
            string[] files = System.IO.Directory.GetFiles(dir_path, "*.dll");
            List<Assembly> ret = new List<Assembly>();

            foreach (string file in files)
            {
                try
                {
                    Assembly.LoadFile(file);
                }
                catch (BadImageFormatException ex)
                {
                    MessageBox.Show(file + " is not a valid DOSSTONED FrontEnd plugin");
                    continue;
                }
                ret.Add(Assembly.LoadFile(file));
            }



            return ret;
        }

        void LoadPlugins(string path)
        {

            int i = 0;
            foreach (Assembly asm in EnumPluginFiles(path))//@"E:\My Documents\Programming\Projects\Platform_Plugins\Plugins"))
            {
                try
                {
                    Assembly_Threads at = new Assembly_Threads(asm);
                    //GroupBox gb = new GroupBox();
                    Panel gb = new Panel();
                    string asmName = at.asm.FullName.Split(',')[0];


                    Thread curT = LoadDLL(asm, new object[] { gb });
                    at.Add(curT);
                    ATs.Add(at);
                    curT.Start();

                    /// Add notification to the GUI

                    //gb.Text = asmName;
                    gb.Width = 600;
                    gb.Height = 400;
                    gb.Margin = new System.Windows.Forms.Padding(3);
                    gb.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;

                    tabControl1.TabPages.Add(asmName);//panelPlugins.Controls.Add(gb);
                    tabControl1.TabPages[i++].Controls.Add(gb);
                    listBoxLoadedPlugins.Items.Add("Plugin: " + asmName + " loaded.");
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, asm.FullName.Split(',')[0]);
                    //Console.WriteLine(ex.Message);
                }
                finally
                {
                    //Console.WriteLine("Press Enter to stop.");
                    //Console.Read();
                    //foreach (Assembly_Threads at in ATs)
                    //{
                    //    UnloadDLL(at);
                    //}
                    //Console.WriteLine("END. Press Enter to exit.");
                    //Console.ReadLine();
                }
            }
        }

        void UnloadPlugins()
        {
            foreach (Assembly_Threads at in ATs)
            {
                UnloadDLL(at);
                /// Add notification to the GUI
                listBoxLoadedPlugins.Items.Add("Plugin: " + at.asm.FullName.Split(',')[0] + " unloaded.");
            }
            ATs.Clear();
            tabControl1.TabPages.Clear();//panelPlugins.Controls.Clear();
        }
    }
}
