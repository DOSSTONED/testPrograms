using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ClassDrcom
{
    public class ClassDr
    {
        static UserControlDr UCDr = null;

        public static void DOSSTONED_FE_LOAD(object ff)
        {
            /// Add the user control first
            Panel f = ff as Panel;
            if (f == null)
                throw new ArgumentException("Argument is not valid, this plugin requires Panel.");
            UCDr = new UserControlDr();
            if (f.InvokeRequired)
            {
                f.Invoke((MethodInvoker)delegate
                {
                    f.Controls.Add(UCDr);
                });
            }
            else
            {
                f.Controls.Add(UCDr);
            }
        }

        public static void DOSSTONED_FE_UNLOAD()
        {
        }
    }
}