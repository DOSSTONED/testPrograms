using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace TrainerTest_1
{
    class Program
    {
        // imports from WinAPI, for more information see http://www.pinvoke.net/ and http://msdn.microsoft.com/

        // http://www.pinvoke.net/default.aspx/kernel32/WriteProcessMemory.html
        // WriteProcessMemory writes memory to a specific address in the target process memory space
        [DllImport("kernel32.dll", EntryPoint = "WriteProcessMemory")]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, [Out] int lpNumberOfBytesWritten);

        // http://www.pinvoke.net/default.aspx/kernel32/ReadProcessMemory.html
        // ReadProcessMemory reads memory from a specified address in the target process memory space
        [DllImport("kernel32.dll", EntryPoint = "ReadProcessMemory")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, [Out] int lpNumberOfBytesRead);
        [DllImport("kernel32.dll", EntryPoint = "ReadProcessMemory")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] IntPtr lpBuffer, int dwSize, [Out] int lpNumberOfBytesRead);

        // http://www.pinvoke.net/default.aspx/kernel32/OpenProcess.html
        // OpenProcess is used to open the process (obviously)
        [DllImport("kernel32.dll", EntryPoint = "OpenProcess")]
        private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        private static int ProcessID = -1; // will hold ID of the game process
        private static IntPtr ProcessHandle = IntPtr.Zero; // will hold handle to the game process

        // Connect function will open the game process
        private static bool Connect()
        {
            Process.EnterDebugMode(); // gain debug privileges

            // GetProcessesByName gets all running processes with the specified name
            Process[] processes = Process.GetProcessesByName("starcraft"); // winmine.exe is Windows XP Minesweeper
            ProcessID = processes[0].Id; // assume the first found process is the correct one, because otherwise 2 instances of the game would be running

            if (ProcessID == 0)
            {
                // game process not found
                Process.LeaveDebugMode();
                return false;
            }

            // open process and save the handle of it
            // we start looking up OpenProcess at MSDN http://msdn.microsoft.com/en-us/library/ms684320(VS.85).aspx
            // "The access to the process object. This access right is checked against the security descriptor for the process. This parameter can be one or more of the process access rights."
            // click the link to "process access rights", http://msdn.microsoft.com/en-us/library/ms684880(v=VS.85).aspx
            // PROCESS_ALL_ACCESS  -  All possible access rights for a process object.
            // yeah, we might aswell use that
            // if we look at http://www.pinvoke.net/default.aspx/kernel32/OpenProcess.html
            // we see that All = 0x001F0FFF
            ProcessHandle = OpenProcess(0x001F0FFF/*PROCESS_ALL_ACCESS*/, false, ProcessID);

            return true;
        }

        private static void Disconnect()
        {
            Process.LeaveDebugMode(); // no need to still have debug privileges
        }

        private static uint addr = 0x00E4D000; // this is the address where time-variable is located in Windows XP Minesweeper, get this with Cheat Engine
        private static uint mineral = 0x57F0DC;
        private static uint mineralDisp = 0x68C218;


        static void Main(string[] args)
        {
            //            string hex = Console.ReadLine();
            //            addr = UInt32.Parse(hex,System.Globalization.NumberStyles.HexNumber);

            //            if (Connect() == false) { return; }
            //            byte[] buffer = new byte[4];
            //            ReadProcessMemory(ProcessHandle, (IntPtr)addr, buffer, buffer.Length, 0);
            //            Console.WriteLine("key: {0}", BitConverter.ToInt32(buffer, 0));

            //            Console.WriteLine("Press any key to continue...");
            //            Console.ReadKey();

            ////            WriteProcessMemory(ProcessHandle,
            //            WriteProcessMemory(ProcessHandle, (IntPtr)addr, BitConverter.GetBytes(9877), 4/*an int is 4 bytes in size*/, 0);

            //            Console.WriteLine("Written...Press any key to continue...");
            //            Console.ReadKey();

            //            Disconnect();

            if (Connect() == false) return;

            byte[] buffer = new byte[4];
            while (true)
            {
                ReadProcessMemory(ProcessHandle, (IntPtr)mineral, buffer, buffer.Length, 0);
                
                Console.WriteLine("mineral: {0}", BitConverter.ToInt32(buffer, 0));
                
                if (Console.ReadKey().Key == ConsoleKey.C) break;

                WriteProcessMemory(ProcessHandle, (IntPtr)mineral, BitConverter.GetBytes(9876), 4, 0);
                WriteProcessMemory(ProcessHandle, (IntPtr)mineralDisp, BitConverter.GetBytes(9876), 4, 0);
            }
            Disconnect();
        }
    }
}
