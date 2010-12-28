using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Powerplan_AutoChange
{

    public static class PowerScheme
    {
        //[DllImport("powrprof.dll", SetLastError = true)]
        //private static extern bool EnumPwrSchemes(PwrSchemesEnumProc lpfnPwrSchemesEnumProc, int lParam);

        #region p/invoke





        #region powerprof
        public static readonly Guid GUID_MAX_POWER_SAVINGS = new Guid(0xA1841308, 0x3541, 0x4FAB, 0xBC, 0x81, 0xF7, 0x15, 0x56, 0xF2, 0x0B, 0x4A);
        public static readonly Guid GUID_MIN_POWER_SAVINGS = new Guid(0x8C5E7FDA, 0xE8BF, 0x4A96, 0x9A, 0x85, 0xA6, 0xE2, 0x3A, 0x8C, 0x63, 0x5C);
        public static readonly Guid GUID_TYPICAL_POWER_SAVINGS = new Guid(0x381B4222, 0xF694, 0x41F0, 0x96, 0x85, 0xFF, 0x5B, 0xB2, 0x60, 0xDF, 0x2E);

        public static readonly Guid NO_SUBGROUP_GUID = new Guid(0xfea3413e, 0x7e05, 0x4911, 0x9a, 0x71, 0x70, 0x03, 0x31, 0xf1, 0xc2, 0x94);
        public static readonly Guid GUID_VIDEO_SUBGROUP = new Guid(0x7516b95f, 0xf776, 0x4464, 0x8c, 0x53, 0x06, 0x16, 0x7f, 0x40, 0xcc, 0x99);


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct USER_POWER_POLICY
        {
            uint Revision;
            POWER_ACTION_POLICY IdleAc;
            POWER_ACTION_POLICY IdleDc;
            uint IdleTimeoutAc;
            uint IdleTimeoutDc;
            byte IdleSensitivityAc;
            byte IdleSensitivityDc;
            byte ThrottlePolicyAc;
            byte ThrottlePolicyDc;
            SYSTEM_POWER_STATE MaxSleepAc;
            SYSTEM_POWER_STATE MaxSleepDc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            uint[] Reserved;
            uint VideoTimeoutAc;
            uint VideoTimeoutDc;
            uint SpindownTimeoutAc;
            uint SpindownTimeoutDc;
            [MarshalAs(UnmanagedType.I1)]
            bool OptimizeForPowerAc;
            [MarshalAs(UnmanagedType.I1)]
            bool OptimizeForPowerDc;
            byte FanThrottleToleranceAc;
            byte FanThrottleToleranceDc;
            byte ForcedThrottleAc;
            byte ForcedThrottleDc;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct MACHINE_POWER_POLICY
        {
            byte Revision;
            SYSTEM_POWER_STATE MinSleepAc;
            SYSTEM_POWER_STATE MinSleepDc;
            SYSTEM_POWER_STATE ReducedLatencySleepAc;
            SYSTEM_POWER_STATE ReducedLatencySleepDc;
            uint DozeTimeoutAc;
            uint DozeTimeoutDc;
            uint DozeS4TimeoutAc;
            uint DozeS4TimeoutDc;
            byte MinThrottleAc;
            byte MinThrottleDc;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            byte[] pad1;
            POWER_ACTION_POLICY OverThrottledAc;
            POWER_ACTION_POLICY OverThrottledDc;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct POWER_ACTION_POLICY
        {
            public POWER_ACTION Action;
            public PowerActionFlags Flags;
            public PowerActionEventCode EventCode;
        }

        enum POWER_ACTION : uint
        {
            PowerActionNone = 0,       // No system power action. 
            PowerActionReserved,       // Reserved; do not use. 
            PowerActionSleep,      // Sleep. 
            PowerActionHibernate,      // Hibernate. 
            PowerActionShutdown,       // Shutdown. 
            PowerActionShutdownReset,  // Shutdown and reset. 
            PowerActionShutdownOff,    // Shutdown and power off. 
            PowerActionWarmEject,      // Warm eject.
        }

        [Flags]
        enum PowerActionFlags : uint
        {
            POWER_ACTION_QUERY_ALLOWED = 0x00000001, // Broadcasts a PBT_APMQUERYSUSPEND event to each application to request permission to suspend operation.
            POWER_ACTION_UI_ALLOWED = 0x00000002, // Applications can prompt the user for directions on how to prepare for suspension. Sets bit 0 in the Flags parameter passed in the lParam parameter of WM_POWERBROADCAST.
            POWER_ACTION_OVERRIDE_APPS = 0x00000004, // Ignores applications that do not respond to the PBT_APMQUERYSUSPEND event broadcast in the WM_POWERBROADCAST message.
            POWER_ACTION_LIGHTEST_FIRST = 0x10000000, // Uses the first lightest available sleep state.
            POWER_ACTION_LOCK_CONSOLE = 0x20000000, // Requires entry of the system password upon resume from one of the system standby states.
            POWER_ACTION_DISABLE_WAKES = 0x40000000, // Disables all wake events.
            POWER_ACTION_CRITICAL = 0x80000000, // Forces a critical suspension.
        }

        [Flags]
        enum PowerActionEventCode : uint
        {
            POWER_LEVEL_USER_NOTIFY_TEXT = 0x00000001, // User notified using the UI. 
            POWER_LEVEL_USER_NOTIFY_SOUND = 0x00000002, // User notified using sound. 
            POWER_LEVEL_USER_NOTIFY_EXEC = 0x00000004, // Specifies a program to be executed. 
            POWER_USER_NOTIFY_BUTTON = 0x00000008, // Indicates that the power action is in response to a user power button press. 
            POWER_USER_NOTIFY_SHUTDOWN = 0x00000010, // Indicates a power action of shutdown/off.
            POWER_FORCE_TRIGGER_RESET = 0x80000000, // Clears a user power button press. 
        }

        enum SYSTEM_POWER_STATE
        {
            PowerSystemUnspecified = 0,
            PowerSystemWorking = 1,
            PowerSystemSleeping1 = 2,
            PowerSystemSleeping2 = 3,
            PowerSystemSleeping3 = 4,
            PowerSystemHibernate = 5,
            PowerSystemShutdown = 6,
            PowerSystemMaximum = 7
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct POWER_POLICY
        {
            public USER_POWER_POLICY user;
            public MACHINE_POWER_POLICY mach;

        }
        public delegate bool PwrSchemesEnumProc(
            uint uiIndex,        // power scheme index
            UInt32 dwName,        // size of the sName string, in bytes
            [MarshalAs(UnmanagedType.LPWStr)]
            string sName,        // name of the power scheme
            UInt32 dwDesc,        // size of the sDesc string, in bytes
            [MarshalAs(UnmanagedType.LPWStr)]
            string sDesc,        // description string
            ref POWER_POLICY pp,    // receives the power policy
            int lParam            // user-defined value
        );

        [DllImport("powrprof.dll", SetLastError = true)]
        public static extern bool EnumPwrSchemes(PwrSchemesEnumProc lpfnPwrSchemesEnumProc, int lParam);

        [DllImport("powrprof.dll", SetLastError = true)]
        public static extern bool PowerReadFriendlyName(string RootPowerKey, Guid SchemeGuid, Guid SubGroupOfPowerSettingsGuid, Guid PowerSettingGuid, ref byte[] Buffer, int BufferSize, out string name);

        [DllImport("powrprof.dll", SetLastError = true)]
        public static extern bool PowerReadPossibleFriendlyName(string RootPowerKey, Guid SubGroupOfPowerSettingsGuid, Guid PowerSettingGuid, int PossibleSettingIndex, ref byte[] Buffer, int BufferSize, out string Name);

        [DllImport("powrprof.dll", SetLastError = true)]
        public static extern bool GetActivePwrScheme(out int puiID);

        //[DllImport("powrprof.dll", EntryPoint = "PowerEnumerate", SetLastError = true)]
        //public static extern UInt32 PowerEnumerate(IntPtr rootPowerKey, IntPtr schemeGuid, IntPtr subgroupOfPowerSettingsGuid, PowerDataAccessor accessFlags, UInt32 index, IntPtr buffer, ref UInt32 bufferSize);

        [DllImport("powrprof.dll")]
        public static extern uint PowerEnumerate(
                    IntPtr RootPowerKey,
                    IntPtr SchemeGuid,
                    Guid SubGroupOfPowerSettingGuid,
                    UInt32 AcessFlags,
                    UInt32 Index,
                    ref Guid Buffer,
                    ref UInt32 BufferSize);

        [DllImport("powrprof.dll")]
        public static extern UInt32 PowerGetActiveScheme(IntPtr UserRootPowerKey, ref IntPtr ActivePolicyGuid);

        [DllImport("powrprof.dll")]
        public static extern uint PowerReadACValue(
                    IntPtr RootPowerKey,
                    IntPtr SchemeGuid,
                    IntPtr SubGroupOfPowerSettingGuid,
                    Guid PowerSettingGuid,
                    ref IntPtr Type,
                    ref IntPtr Buffer,
                    ref UInt32 BufferSize);


        #endregion

        #region Enumeration

        internal enum PowerDataAccessor
        {
            /// <summary>
            /// Used with PowerSettingAccessCheck to check for group policy
            /// overrides for AC power settings. 
            /// </summary>
            AccessACPowerSettingIndex = 0,   // 0x0

            /// <summary>
            /// Used with PowerSettingAccessCheck to check for group policy 
            /// overrides for DC power settings. 
            /// </summary>
            AccessDCPowerSettingIndex = 1,// 0x1

            /// <summary>
            /// Used to enumerate power schemes with PowerEnumerate and with
            /// PowerSettingAccessCheck to check for restricted access to 
            /// specific power schemes. 
            /// </summary>
            AccessScheme = 16,// 0x10

            /// <summary>
            /// Used to enumerate subgroups with PowerEnumerate. 
            /// </summary>
            AccessSubgroup = 17,// 0x11

            /// <summary>
            /// Used to enumerate individual power settings with 
            /// PowerEnumerate. 
            /// </summary>
            AccessIndividualSetting = 18,// 0x12

            /// <summary>
            /// Used with PowerSettingAccessCheck to check for 
            /// group policy overrides for active power schemes. 
            /// </summary>
            AccessActiveScheme = 19, // 0x13

            /// <summary>
            /// Used with PowerSettingAccessCheck to check for 
            /// restricted access for creating power schemes. 
            /// </summary>
            AccessCreateScheme = 20 // 0x14

        };
        #endregion

        #endregion


    }
}