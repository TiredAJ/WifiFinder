using System.Collections.Generic;
using WifiScannerPedwar.Services;
using WifiScannerLib;
using Microsoft.CodeAnalysis;

using SDD = System.Diagnostics.Debug;
using System.Linq;

namespace WifiScannerPedwar.ViewModels;

public class MainViewModel : ViewModelBase
{
    private static WifiService WS;
    private static Avalonia.Threading.DispatcherTimer Amserwr;

    public MainViewModel(IWS? _IWScanner)
    {
        //checks if the inputted IWS isn't null (it is on initialisation/perm requesting)
        if (_IWScanner != null)
        {Initialise(_IWScanner);}
        else
        {/*something to do while waiting for perms?*/}
    }

    private static void Tick(object? sender, System.EventArgs e)
    {
        //calls add items and asks WS to generate new results
        WifiList.AddItems(WS.GetItems());
    }

    public static void Initialise(IWS _IWScanner)
    {
        //creates a new wifi service using the selected IWS
        WS = new WifiService(_IWScanner);

        //creates a dispatch timer for scheduling scans
        //Amserwr = new Avalonia.Threading.DispatcherTimer();
        Amserwr = new Avalonia.Threading.DispatcherTimer(Avalonia.Threading.DispatcherPriority.Background);

        //initial data nabbing
        var Data = WS.GetItems();

        //sets up the dispatchtimer
        Amserwr.Interval = new System.TimeSpan(0, 0, 5);
        Amserwr.Tick += Tick;
        Amserwr.Start();

        //generates new data holder
        WifiList = new WifiViewModel(Data);

        //temp
        SDD.WriteLine("Item's gought");
    }

    //data holder
    public static WifiViewModel WifiList { get; private set; } = new WifiViewModel();
}
