using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace hn.Common
{
    public static class ADAccountHelp
    {
        //#region LIS Logon  Tom.Tan

        //public const int TOKEN_ADJUST_PRIVILEGES = 0x0020;
        //public const int TOKEN_QUERY = 0x0008;
        //public const int SE_PRIVILEGE_ENABLED = 0x00000002;

        //public const string SE_TCB_NAME = "SeTcbPrivilege";

        //[DllImport("Kernel32.dll")]
        //public static extern int GetLastError();

        //[DllImport("advapi32.dll", SetLastError = true)]
        //public static extern bool LogonUser(String lpszUsername, String lpszDomain, String lpszPassword,
        //    int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        //[DllImport("advapi32.dll", SetLastError = true)]    // Tom.Tan
        //public static extern bool LogonUser(String lpszUsername, String lpszDomain, String lpszPassword,
        //    int dwLogonType, int dwLogonProvider);

        //[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        //public extern static bool CloseHandle(IntPtr handle);

        //[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        //public extern static bool DuplicateHandle(IntPtr handle, ref IntPtr destHandler);

        //[StructLayout(LayoutKind.Sequential, Pack = 1)]
        //public struct LUID
        //{
        //    public UInt32 LowPart;
        //    public int HighPart;
        //}

        //public struct LUID_AND_ATTRIBUTES
        //{
        //    public LUID Luid;
        //    public UInt32 Attributes;
        //};

        //[StructLayout(LayoutKind.Sequential, Pack = 1)]
        //public struct TOKEN_PRIVILEGES
        //{
        //    public UInt32 PrivilegeCount;
        //    public LUID_AND_ATTRIBUTES Privileges;
        //}

        //[DllImport("advapi32.dll")]
        //public static extern int LookupPrivilegeValue(
        //  string lpSystemName,
        //  string lpName,
        //  ref LUID lpLuid
        //);

        //[DllImport("advapi32.dll", CharSet = CharSet.Ansi)]
        //public static extern int AdjustTokenPrivileges(
        //  IntPtr TokenHandle,
        //  bool DisableAllPrivileges,
        //  ref TOKEN_PRIVILEGES NewState,
        //  int BufferLength,
        //  ref TOKEN_PRIVILEGES PreviousState,
        //  ref int ReturnLength
        //);

        //public const int LOGON32_PROVIDER_DEFAULT = 0;
        ////This parameter causes LogonUser to create a primary token.
        //public const int LOGON32_LOGON_INTERACTIVE = 2;

        //public static void SetSE_TCB_NAMEPrivilege()
        //{
        //    System.Security.Principal.WindowsIdentity winID =
        //        System.Security.Principal.WindowsIdentity.GetCurrent();

        //    SetSE_TCB_NAMEPrivilege(winID.Token);
        //}

        //public static void SetSE_TCB_NAMEPrivilege(IntPtr hToken)
        //{
        //    TOKEN_PRIVILEGES tkp = new TOKEN_PRIVILEGES();
        //    TOKEN_PRIVILEGES srcTkp = new TOKEN_PRIVILEGES();

        //    LookupPrivilegeValue(null, SE_TCB_NAME, ref tkp.Privileges.Luid);
        //    tkp.PrivilegeCount = 1;
        //    tkp.Privileges.Attributes = SE_PRIVILEGE_ENABLED;

        //    srcTkp.Privileges.Luid = tkp.Privileges.Luid;

        //    int returnLength = 0;
        //    int result = AdjustTokenPrivileges(hToken, false, ref tkp, 20, ref srcTkp, ref returnLength);

        //    CloseHandle(hToken);
        //}

        //#endregion


        #region EL login
        public const int TOKEN_ADJUST_PRIVILEGES = 0x0020;
        public const int TOKEN_QUERY = 0x0008;
        public const int SE_PRIVILEGE_ENABLED = 0x00000002;
        public const string SE_TCB_NAME = "SeTcbPrivilege";
        public const int LOGON32_PROVIDER_DEFAULT = 0;
        //This parameter causes LogonUser to create a primary token.
        public const int LOGON32_LOGON_INTERACTIVE = 2;

        [DllImport("Kernel32.dll")]
        public static extern int GetLastError();

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool LogonUser(String lpszUsername, String lpszDomain, String lpszPassword,
            int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public extern static bool CloseHandle(IntPtr handle);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public extern static bool DuplicateHandle(IntPtr handle, ref IntPtr destHandler);

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct LUID
        {
            public UInt32 LowPart;
            public int HighPart;
        }

        public struct LUID_AND_ATTRIBUTES
        {
            public LUID Luid;
            public UInt32 Attributes;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TOKEN_PRIVILEGES
        {
            public UInt32 PrivilegeCount;
            public LUID_AND_ATTRIBUTES Privileges;
        }

        [DllImport("advapi32.dll")]
        public static extern int LookupPrivilegeValue(
          string lpSystemName,
          string lpName,
          ref LUID lpLuid
        );

        [DllImport("advapi32.dll", CharSet = CharSet.Ansi)]
        public static extern int AdjustTokenPrivileges(
          IntPtr TokenHandle,
          bool DisableAllPrivileges,
          ref TOKEN_PRIVILEGES NewState,
          int BufferLength,
          ref TOKEN_PRIVILEGES PreviousState,
          ref int ReturnLength
        );

        #endregion

    }
}
