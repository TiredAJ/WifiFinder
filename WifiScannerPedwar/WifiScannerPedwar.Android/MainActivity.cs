using Android;
using Android.App;
using Android.Content.PM;
using Avalonia;
using Avalonia.Android;
using Avalonia.ReactiveUI;
using System.Threading.Tasks;
using WifiScannerLib;
using WifiScannerPedwar.Services;
using WifiScannerPedwar.ViewModels;
using System.Collections.Generic;
using Android.Content;
using Android.Service.Controls.Templates;
using SDD = System.Diagnostics.Debug;

namespace WifiScannerPedwar.Android;

//may be kinda cheaty, but here is where we grab app context and do shit
//that requires android "activity" shenangians

[Activity(
    Label = "WifiScannerPedwar.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{
    Context? CTX;

    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        List<string> TempPerms = new List<string>();
        CTX = this.ApplicationContext;

#if ANDROID23_0_OR_GREATER
        //goes through and checks what permissions we have vs what we need
        //what we need but don't have is added to a list
        #region Permission Checks
        if (CTX.CheckSelfPermission(Manifest.Permission.AccessWifiState) == Permission.Denied)
        { TempPerms.Add(Manifest.Permission.AccessWifiState); }

        if (CTX.CheckSelfPermission(Manifest.Permission.AccessFineLocation) == Permission.Denied)
        { TempPerms.Add(Manifest.Permission.AccessFineLocation); }

        if (CTX.CheckSelfPermission(Manifest.Permission.ChangeWifiState) == Permission.Denied)
        { TempPerms.Add(Manifest.Permission.ChangeWifiState); }
        #endregion

        //...that list is then sent to the system to prompt the user for perms
        if (TempPerms.Count > 0)
        { RequestPermissions(TempPerms.ToArray(), 2); }
        else if (CTX != null)
        {
            //if we already have perms, it goes straight to setting the IWS to android
            WifiScannerPedwar.App.IWScanner = new WifiScannerLib.AndroidWS(CTX);
        }

#endif
        //default boilerplate stuff idk
        return base.CustomizeAppBuilder(builder)
            .WithInterFont()
            .UseReactiveUI()
            .LogToTrace(Avalonia.Logging.LogEventLevel.Fatal);
    }

    //this is called when the result of the permission request is received
    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
    {
        //SDD.WriteLine("**** Permissions Results are in ****");

        base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        //checks if context isn't null, then sets IWS stuff to android version
        if (CTX != null)
        {
            WifiScannerPedwar.App.IWScanner = new WifiScannerLib.AndroidWS(CTX);
            WifiScannerPedwar.ViewModels.MainViewModel.Initialise(new WifiScannerLib.AndroidWS(CTX));
        }
        else
        { throw new System.Exception("**** Heyo, CTX was null pall ****"); }
    }
}
