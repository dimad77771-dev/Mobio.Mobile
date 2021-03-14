using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using OneBuilder.Mobile;
using UIKit;

namespace Mobio.Mobile.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            try
            {
                AppDomain.CurrentDomain.UnhandledException += logUnhandledException;
                UIApplication.Main(args, null, "AppDelegate");
            }
            catch (Exception ex)
            {
                //Utilities2.LogUnhandledException(ex);
                UnhandledExceptionProccesing.LogUnhandledException(ex);
            }
        }

        static void logUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //Utilities2.LogUnhandledException(e.ExceptionObject as Exception);
            UnhandledExceptionProccesing.LogUnhandledException(e.ExceptionObject as Exception);
        }
    }
}
