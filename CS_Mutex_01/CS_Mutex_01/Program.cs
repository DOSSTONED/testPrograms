using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CS_Mutex_01
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Boolean createdNew; //返回是否赋予了使用线程的互斥体初始所属权
            System.Threading.Mutex instance = new System.Threading.Mutex(true, "DOSSTONED_GLOBAL_MUTEX", out createdNew); //同步基元变量
            if (createdNew) //赋予了线程初始所属权，也就是首次使用互斥体
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
                instance.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("已经启动了一个程序，请先退出！","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
                Application.Exit();
            }

        }
    }
}
