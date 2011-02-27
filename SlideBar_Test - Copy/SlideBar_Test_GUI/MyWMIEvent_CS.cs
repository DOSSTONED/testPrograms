using System;
using System.Management;

namespace SlideBar_Test_GUI
{
    public class WMIReceiveEvent
    {
        public ManagementEventWatcher watcher = null;
        private byte[] formerEvtBytes = null;


        public WMIReceiveEvent()
        {
            ManagementScope x = new System.Management.ManagementScope("root\\WMI");

            WqlEventQuery query = new WqlEventQuery("SELECT * FROM WMIACPIEvent");

            watcher = new ManagementEventWatcher(x, query);
            Console.WriteLine("Waiting for an event...");

            
        }

        public WMIReceiveEvent(string str)
        {
            ManagementScope x = new System.Management.ManagementScope("root\\WMI");

            WqlEventQuery query = new WqlEventQuery("SELECT * FROM WMIACPIEvent");

            watcher = new ManagementEventWatcher(x, query);
            Console.WriteLine("Waiting for an event...");

            if (str == "AutoReg")
            {
                watcher.EventArrived += new EventArrivedEventHandler(HandleEvent);

                // Start listening for events
                watcher.Start();
            }
        }

        

        private void HandleEvent(object sender, EventArrivedEventArgs e)
        {
            Console.WriteLine("WMIACPIEvent event occurred.");
            ManagementBaseObject mbo = e.NewEvent;
            byte[] EvtBytes = mbo["EvtBytes"] as byte[];

            if (formerEvtBytes == null)
            {
                formerEvtBytes = EvtBytes;
            }
            else
            {
                for (int i = 0; i < EvtBytes.Length; i++)
                {
                    if (EvtBytes[i] != formerEvtBytes[i])
                    {
                        Console.WriteLine("Byte changed on {0}, which changed from {1} to {2}.", i, formerEvtBytes[i], EvtBytes[i]);
                    }
                }
                formerEvtBytes = EvtBytes;
            }
        }
        
        
        ~WMIReceiveEvent()
        {
            if (watcher != null)
            {
                watcher.Stop();
            }
        }
    }
}