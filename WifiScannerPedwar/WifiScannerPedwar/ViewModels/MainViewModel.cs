using Avalonia.Platform.Storage;

using WifiScannerLib;

using WifiScannerPedwar.Services;

using SDD = System.Diagnostics.Debug;


namespace WifiScannerPedwar.ViewModels;

public class MainViewModel : ViewModelBase
{
    private static WifiService WS;
    public static bool IsInitialised = false;

    public string APCountText { get; set; } = "No APs scanned atm";

    public MainViewModel(IWS? _IWScanner)
    {
        //checks if the in-putted IWS isn't null (it is on initialisation/perm requesting)
        if (_IWScanner != null)
        { Initialise(_IWScanner); }
        else
        {/*something to do while waiting for perms*/}
    }

    public MainViewModel()
    { }

    public static void Initialise(IWS _IWScanner)
    {
        //creates a new wifi service using the selected IWS
        WS = new WifiService(_IWScanner);

        IsInitialised = true;

        //initial data nabbing
        //WS.TriggerScan();
    }

    public void TriggerScan()
    {
        SDD.WriteLine("MVM Scan triggered!");
        WS.TriggerScan();
    }

    public void SaveData(IStorageProvider _ISP)
    { _ = WS.SaveToFile(_ISP); }
}
