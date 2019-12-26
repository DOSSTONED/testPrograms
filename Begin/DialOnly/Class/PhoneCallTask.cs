using System;
using System.Security;
using System.Threading;
using System.Windows;

namespace DialOnly.Class
{
    /// <summary>Allows an application to launch the Phone application. Use this to allow users to make a phone call from your application.</summary>
    public sealed class PhoneCallTask
    {
        /// <summary>Gets or sets the name that is displayed when the Phone application is launched.</summary>
        /// <returns>Type: 
        /// <see cref="T:System.String" />. The name that is displayed when the Phone application is launched.</returns>
        public string DisplayName
        {
            get;
            set;
        }
        /// <summary>Gets or sets the phone number that is dialed when the Phone application is launched.</summary>
        /// <returns>Type: 
        /// <see cref="T:System.String" />. The phone number that is dialed when the Phone application is launched.</returns>
        public string PhoneNumber
        {
            get;
            set;
        }
        static PhoneCallTask()
        {
        }
        /// <summary>Shows the Phone application.</summary>
        [SecuritySafeCritical]
        public void Show()
        {
            if (!ChooserHelper.NavigationInProgressGuard(delegate
            {
                this.Show();
            }
            ))
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(new WaitCallback(PhoneCallTask.PhoneDial), this);
        }
        [SecuritySafeCritical]
        private static void PhoneDial(object phoneCallTask)
        {
            PhoneCallTask phoneCallTask2 = phoneCallTask as PhoneCallTask;
            string phoneNumber = phoneCallTask2.PhoneNumber;
            string arg_14_0 = phoneCallTask2.DisplayName;
            string.IsNullOrEmpty(phoneNumber);
        }
    }
}
