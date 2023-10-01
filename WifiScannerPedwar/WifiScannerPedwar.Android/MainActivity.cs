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

        #region Permission Checks
        if (CTX.CheckSelfPermission(Manifest.Permission.AccessWifiState) == Permission.Denied)
        { TempPerms.Add(Manifest.Permission.AccessWifiState); }

        if (CTX.CheckSelfPermission(Manifest.Permission.AccessFineLocation) == Permission.Denied)
        { TempPerms.Add(Manifest.Permission.AccessFineLocation); }

        if (CTX.CheckSelfPermission(Manifest.Permission.ChangeWifiState) == Permission.Denied)
        { TempPerms.Add(Manifest.Permission.ChangeWifiState); }
        #endregion

        if (TempPerms.Count > 0)
        { RequestPermissions(TempPerms.ToArray(), 2); }
        else if (CTX != null)
        {WifiScannerPedwar.App.IWScanner = new WifiScannerLib.AndroidWS(CTX);}

#endif
        return base.CustomizeAppBuilder(builder)
            .WithInterFont()
            .UseReactiveUI();
    }

    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
    {
        SDD.WriteLine("**** Permissions Results are in ****");

        base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        if (CTX != null)
        {WifiScannerPedwar.App.IWScanner = new WifiScannerLib.AndroidWS(CTX);}
        else
        { throw new System.Exception("**** Heyo, CTX was null pall ****"); }
    }
}
