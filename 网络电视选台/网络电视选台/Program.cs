using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using System.Windows.Forms;
using Vlc.DotNet.Core;

namespace 网络电视选台
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Set libvlc.dll and libvlccore.dll directory path
            //VlcContext.LibVlcDllsPath = CommonStrings.LIBVLC_DLLS_PATH_DEFAULT_VALUE_X86;
            //Set the vlc plugins directory path
            //VlcContext.LibVlcPluginsPath = CommonStrings.PLUGINS_PATH_DEFAULT_VALUE_X86;
            VlcContext.LibVlcDllsPath = @"VLCPortable\App\vlc\";
            VlcContext.LibVlcPluginsPath = @"VLCPortable\App\vlc\plugins\";

            //Set the startup options
            VlcContext.StartupOptions.IgnoreConfig = true;
            VlcContext.StartupOptions.LogOptions.LogInFile = true;
            VlcContext.StartupOptions.LogOptions.ShowLoggerConsole = true;
            VlcContext.StartupOptions.LogOptions.Verbosity = VlcLogVerbosities.Debug;
            

            //Initialize the VlcContext
            VlcContext.Initialize(); 
            Application.Run(new Form1());


            //Close the VlcContext
            VlcContext.CloseAll(); 
        }
    }
}
