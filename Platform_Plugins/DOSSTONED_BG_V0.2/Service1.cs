using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Reflection;
using System.Threading;

namespace DOSSTONED_BG_V0._2
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        List<Assembly_Threads> ATs = null;

        protected override void OnStart(string[] args)
        {
            ATs = new List<Assembly_Threads>();
            try
            {
                foreach (Assembly asm in DLLReg.EnumPluginFiles(@"C:\Users\Public\Documents\DOSSTONED_BG\Plugins"))
                {
                    Assembly_Threads at = new Assembly_Threads(asm);
                    Thread curT = DLLReg.LoadDLL(asm, null);
                    at.Add(curT);
                    ATs.Add(at);
                    curT.Start();
                    
                }
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(@"E:\TEMP\DOSSTONED_BG.txt", "\r\n"+ex.Message + "\r\n");
                System.IO.File.AppendAllText(@"E:\TEMP\DOSSTONED_BG.txt", ex.StackTrace + "\r\n");
                Stop();
            }
        }

        protected override void OnStop()
        {
            try
            {
                foreach (Assembly_Threads at in ATs)
                {
                    DLLReg.UnloadDLL(at);
                }

            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(@"E:\TEMP\DOSSTONED_BG.txt", "\r\n" + ex.Message + "\r\n");
                System.IO.File.AppendAllText(@"E:\TEMP\DOSSTONED_BG.txt", ex.StackTrace + "\r\n");
            }

        }
    }
}
