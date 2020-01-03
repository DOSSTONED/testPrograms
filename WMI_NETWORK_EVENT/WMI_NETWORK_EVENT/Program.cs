using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Media;
using System.Threading;
using System.Runtime.InteropServices;
using System.Reflection;


namespace WMI_NETWORK_EVENT
{
    class Program
    {
        //[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        //internal static extern bool MessageBeep(int type);

        static void Main(string[] args)
        {
            try
            {
                /// Add a watcher to the event, also add the watcher to the list
                /// 
                List<ManagementEventWatcher> watchers = new List<ManagementEventWatcher>();
                watchers.Add(addWatcher("MSNdis_StatusMediaConnect"));
                watchers.Add(addWatcher("MSNdis_StatusMediaDisconnect"));
                watchers.Add(addWatcher("MSNdis_StatusNetworkChange"));
                
                Console.WriteLine("Waiting for an event... Press Enter to stop.");
                Thread t = new Thread(new ThreadStart(delegate 
                    {
                    LoadDllMethod("DLL_Test1.Class1", "main", @"WMI_NETWORK_EVENT\DLL_Test1\bin\Debug\DLL_Test1.dll", null); 
                }));

                t.Start();

                playSound(null);
                // Cancel the event subscription
                Console.Read();
                foreach (ManagementEventWatcher watch in watchers)
                {
                    if (watch != null)
                        watch.Stop();
                }

                if (t.IsAlive)
                    t.Abort();

                return;
            }
            catch (ManagementException err)
            {
                Console.WriteLine("An error occurred while trying to receive an event: " + err.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static string LoadDllMethod(string title_class, string title_void, string path, object[] parameters)
		{
			Assembly u = Assembly.LoadFile(path);
			Type t = u.GetType(title_class);
			if (t != null)
			{
				MethodInfo m = t.GetMethod(title_void);
                if (m != null)
                {
                    if ((parameters != null) && (parameters.Length >= 1))
                    {
                        object[] myparam = new object[1];
                        myparam[0] = parameters;
                        return (string)m.Invoke(null, myparam);
                    }

                    else
                        return (string)m.Invoke(null, null);
                }
			}
			Exception ex = new Exception("method/class not found");
			throw ex;
		}

        static SoundPlayer sp = new SoundPlayer();
        static void playSound(SystemSound sound)
        {
            sp.Stream = Properties.Resources.UI_Ping;
            sp.Play();
        }

        static ManagementEventWatcher addWatcher(ManagementScope scope, WqlEventQuery query)
        {
            ManagementEventWatcher watcher = new ManagementEventWatcher(scope, query);
            watcher.EventArrived += new EventArrivedEventHandler(watcher_EventArrived);
            watcher.Start();
            return watcher;
        }

        static void watcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            string eventName = e.NewEvent["__CLASS"] as string;
            playSound(null);
            switch (eventName)
            {
                case "MSNdis_StatusNetworkChange":
                    break;
                case "MSNdis_StatusMediaConnect":
                case "MSNdis_StatusMediaDisconnect":
                    Console.WriteLine("\r\n{0} event occurred.", e.NewEvent["__CLASS"]);
                    foreach (PropertyData pd in e.NewEvent.Properties)
                    {
                        Console.WriteLine("[{0}] is {1}", pd.Name, pd.Value);
                    }
                    break;
                default:
                    break;
            }
            //throw new NotImplementedException();
        }

        static ManagementEventWatcher addWatcher(string queryString)
        {
            ManagementScope scope = new ManagementScope("root\\wmi");
            WqlEventQuery query = new WqlEventQuery("SELECT * FROM " + queryString);
            return addWatcher(scope, query);
        }
    }
}
