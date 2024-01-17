using Avalonia.Platform.Storage;

using ReactiveUI;

using System.Collections.Generic;
using System.Linq;

using WifiScannerLib;

using WifiScannerPedwar.Services;

using SDD = System.Diagnostics.Debug;


namespace WifiScannerPedwar.ViewModels;

public class MainViewModel : ViewModelBase
{
    private static WifiService WS;
    public static bool IsInitialised = false;

    private string _APCountText = "No APs scanned :/";

    public string APCountText
    {
        get => _APCountText;
        private set => this.RaiseAndSetIfChanged(ref _APCountText, value);
    }

    public MainViewModel(IWS? _IWScanner)
    {
        //checks if the in-putted IWS isn't null (it is on initialisation/perm requesting)
        if (_IWScanner != null)
        { Initialise(_IWScanner); }
        else
        {/*something to do while waiting for perms*/}

        WS.CountUpdated += CountUpdated;
        WS.Filter = (In) =>
        {
            List<string> AllowedSSIDs = new List<string>()
            { "usw", "usw-ce", "usw-guest", "usw-openday", "eduroam"};

            In = In.Where(X => AllowedSSIDs.Contains(X.Value.SSID.ToLower()))
                    .ToDictionary(X => X.Key, Y => Y.Value);

            return In;
        };
    }

    public MainViewModel()
    { }

    public static void Initialise(IWS _IWScanner)
    {
        //creates a new wifi service using the selected IWS
        WS = new WifiService(_IWScanner);

        IsInitialised = true;
    }

    private void CountUpdated(object? sender, APCount e)
    {
        if (e.Count == 0)
        { APCountText = "Ready to collect data!"; }
        else
        { APCountText = $"{e.Count} AP('s) collected!"; }
    }

    public void TriggerScan()
    {
        SDD.WriteLine("MVM Scan triggered!");
        WS.CollectData();
    }

    public void SaveData(IStorageProvider _ISP)
    { _ = WS.SaveToFile(_ISP); }

    public void ClearData()
    { WS.ClearData(); }
}
