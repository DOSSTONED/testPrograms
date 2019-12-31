using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell.Interop;
using Microsoft.Phone.TaskModel.Interop;
using System;
using System.IO;
using System.Security;
using System.Windows;

namespace DialOnly.Class
{
    public class ChooserHelper
    {
        private const string platformDataFolderName = "PlatformData";
        private static string platformDataFolderPath;
        //[SecurityCritical]
        //internal static string GetNewFilepath(string prefix, string extension)
        //{
        //    if (string.IsNullOrEmpty(ChooserHelper.platformDataFolderPath))
        //    {
        //        HostInfo hostInfo = new HostInfo();
        //        ChooserHelper.platformDataFolderPath = Path.Combine(hostInfo.AppDataFolder, "PlatformData");
        //        Directory.CreateDirectory(ChooserHelper.platformDataFolderPath);
        //    }
        //    string str = Guid.NewGuid().ToString();
        //    return Path.Combine(ChooserHelper.platformDataFolderPath, prefix + str + extension);
        //}
        internal static bool NavigationInProgressGuard(Action action)
        {
            if (Deployment.Current != null && !Deployment.Current.Dispatcher.CheckAccess())
            {
                Deployment.Current.Dispatcher.BeginInvoke(action);
                return false;
            }
            PhoneApplicationFrame phoneApplicationFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            //if (phoneApplicationFrame != null)// && phoneApplicationFrame.NavigationService.IsNavigationInProgress)
            //{
            //    throw new InvalidOperationException("Not allowed to call Show() when a navigation is in progress");
            //}
            //if (ChooserListener.IsChooserPending)
            //{
            //    throw new InvalidOperationException("Not allowed to call Show() multiple times before a Chooser returns");
            //}
            return true;
        }
        //internal static void Invoke(Uri appUri, byte[] buffer, IChooser chooser)
        //{
        //    ShellPageManager.Instance.OnChooserInvoked(chooser);
        //    ShellPageManager.Instance.InvokeExternalPage(appUri.ToString(), buffer);
        //}
        //internal static void Navigate(Uri appUri, ParameterPropertyBag ppb)
        //{
        //    byte[] args = ChooserHelper.Serialize(ppb);
        //    ShellPageManager.Instance.NavigateToExternalPage(appUri.ToString(), args);
        //}
        //internal static void LaunchSession(string appUri)
        //{
        //    ShellPageManager.Instance.LaunchSession(appUri);
        //}
        //internal static byte[] Serialize(ParameterPropertyBag ppb)
        //{
        //    uint serializedSize = ppb.SerializedSize;
        //    byte[] array = new byte[(int)((UIntPtr)serializedSize)];
        //    ppb.Serialize(array, serializedSize);
        //    return array;
        //}
        //[SecuritySafeCritical]
        //public static void FillCommonPhotoProperties(ParameterPropertyBag ppb, string filePrefix)
        //{
        //    ParameterProperty parameterProperty = ppb.CreateProperty("DestinationFilePath");
        //    string newFilepath = ChooserHelper.GetNewFilepath(filePrefix, ".jpg");
        //    parameterProperty.StringValue = newFilepath;
        //}
        //[SecuritySafeCritical]
        //internal static PhotoResult PathParameterToPhotoResult(ParameterProperty local)
        //{
        //    string stringValue = local.StringValue;
        //    PhotoStream chosenPhoto = new PhotoStream(stringValue);
        //    return new PhotoResult
        //    {
        //        ChosenPhoto = chosenPhoto,
        //        OriginalFileName = stringValue
        //    };
        //}
        //internal static byte[] ReadStream(Stream s)
        //{
        //    int i = (int)s.Length;
        //    byte[] array = new byte[i];
        //    int num = 0;
        //    while (i > 0)
        //    {
        //        int num2 = s.Read(array, num, i);
        //        num += num2;
        //        i -= num2;
        //    }
        //    return array;
        //}
    }
}
