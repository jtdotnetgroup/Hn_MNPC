using hn.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hn.Client.Service
{
    public static class ADLogin
    {

        public static bool Login(string domainName,string userName,string password)
        {
            //ADAccountHelp.SetSE_TCB_NAMEPrivilege();

            try
            {
                IntPtr tokenHandle = IntPtr.Zero;

                bool returnValue = ADAccountHelp.LogonUser(userName, domainName, password,
                    ADAccountHelp.LOGON32_LOGON_INTERACTIVE, ADAccountHelp.LOGON32_PROVIDER_DEFAULT,
                    ref tokenHandle);

                int strError = ADAccountHelp.GetLastError();

               // LogHelper.WriteLog("错误码：" + strError);

                ADAccountHelp.CloseHandle(tokenHandle);
                return returnValue;
            }
            catch(Exception ex)
            {
                LogHelper.WriteLog(ex);
                return false;
            }
        }

    }
}
