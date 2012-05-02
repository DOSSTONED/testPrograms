using Microsoft.Phone.Shell.Interop;
using System;
using System.IO;
using System.Runtime.Serialization;

namespace DialOnly.Class
{
    public static class ChooserListener
    {
        //private static IChooser _warmChooser;
        //private static ChooserInfo _pendingChooserInfo;
        //private static bool _fPendingChooserDeserialized;
        //private static InvokeReturningEventArgs _pendingChooserArgs;
        //private static bool _isInitialized;
        //internal static bool IsChooserPending
        //{
        //    get
        //    {
        //        return ChooserListener._warmChooser != null;
        //    }
        //}
        //static ChooserListener()
        //{
        //    ChooserListener.Initialize();
        //}
        //public static void Initialize()
        //{
        //    if (!ChooserListener._isInitialized)
        //    {
        //        ShellPageManager instance = ShellPageManager.Instance;
        //        if (instance != null)
        //        {
        //            ShellPageManagerCallback expr_16 = instance.ShellPageManagerCallback;
        //            expr_16.OnInvokeReturningEventHandler = (EventHandler<InvokeReturningEventArgs>)Delegate.Combine(expr_16.OnInvokeReturningEventHandler, new EventHandler<InvokeReturningEventArgs>(ChooserListener.OnInvokeReturning));
        //        }
        //        ChooserListener._isInitialized = true;
        //    }
        //}
        //internal static void RegisterChooser(IChooser chooser)
        //{
        //    if (ChooserListener._warmChooser != null)
        //    {
        //        throw new InvalidOperationException("Not allowed to call Show() multiple times before an invocation returns");
        //    }
        //    ChooserListener._warmChooser = chooser;
        //    ChooserListener._pendingChooserInfo = null;
        //    ChooserListener._pendingChooserArgs = null;
        //    ChooserListener._fPendingChooserDeserialized = false;
        //}
        //internal static void DeregisterChooser()
        //{
        //    ChooserListener._warmChooser = null;
        //}
        //internal static void FireChooserComplete(InvokeReturningEventArgs chooserArgs, IChooser chooser)
        //{
        //    if (chooserArgs.stateBuffer != null)
        //    {
        //        chooser.Deserialize(new MemoryStream(chooserArgs.stateBuffer));
        //    }
        //    chooser.OnInvokeReturned(chooserArgs.returnBuffer, null);
        //}
        //internal static bool IsChooserCompletePending(IChooser chooser, Delegate handler, out InvokeReturningEventArgs args)
        //{
        //    bool result = false;
        //    args = null;
        //    if (ChooserListener._pendingChooserInfo != null && string.Equals(ChooserInfo.ChooserTypeFromChooser(chooser), ChooserListener._pendingChooserInfo.ChooserType) && ChooserListener._pendingChooserInfo.Observers != null)
        //    {
        //        string key = ChooserInfo.ObserverFromDelegate(handler);
        //        if (ChooserListener._pendingChooserInfo.Observers.ContainsKey(key))
        //        {
        //            if (!ChooserListener._fPendingChooserDeserialized)
        //            {
        //                if (ChooserListener._pendingChooserArgs.stateBuffer != null)
        //                {
        //                    chooser.Deserialize(new MemoryStream(ChooserListener._pendingChooserArgs.stateBuffer));
        //                }
        //                ChooserListener._fPendingChooserDeserialized = true;
        //            }
        //            ChooserListener._pendingChooserInfo.Observers.Remove(key);
        //            args = ChooserListener._pendingChooserArgs;
        //            result = true;
        //        }
        //    }
        //    return result;
        //}
        //private static void OnInvokeReturning(object sender, InvokeReturningEventArgs args)
        //{
        //    if (ChooserListener._warmChooser != null)
        //    {
        //        ChooserListener.FireChooserComplete(args, ChooserListener._warmChooser);
        //        return;
        //    }
        //    MemoryStream stream = new MemoryStream(args.correlationBlob);
        //    DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(ChooserInfo));
        //    ChooserListener._pendingChooserInfo = (ChooserInfo)dataContractSerializer.ReadObject(stream);
        //    ChooserListener._pendingChooserArgs = args;
        //}
    }
}
