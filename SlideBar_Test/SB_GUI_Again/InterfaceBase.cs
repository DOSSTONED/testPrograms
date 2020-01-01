using System;
using System.Management;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace SBarHook
{
    public class InterfaceBase
    {
        public event DebugInfoHandler DebugInfoEvent
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                this.DebugInfoEvent += value;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                this.DebugInfoEvent -= value;
            }
        }
        public void DebugInfoTrace(string dbgInfo)
        {
            try
            {
                //this.DebugInfoEvent += new DebugInfoHandler(dbgInfo);
                //if (this.DebugInfoEvent != null)
                //{
                //    this.DebugInfoEvent(dbgInfo);
                //}
                
            }
            catch (Exception arg_16_0)
            {
                Exception exception = arg_16_0;
                MessageBox.Show("Debug Information: " + dbgInfo + "\r\n" + exception.ToString());
            }
        }
        public void DebugInfoTrace(ManagementException ex)
        {
            this.DebugInfoTrace(string.Concat(new string[]
			{
				"ManagementException: ", 
				ex.ErrorCode.ToString(), 
				" (0x", 
				ex.ErrorCode.ToString("X"), 
				")"
			}));
            this.DebugInfoTrace(ex);
        }
        public void DebugInfoTrace(Exception ex)
        {
            this.DebugInfoTrace("Exception: " + ex.Message);
            this.DebugInfoTrace("Detailed exception info: " + ex.ToString() + "\r\n");
        }
    }
}
