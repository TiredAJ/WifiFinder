using System.Collections.Generic;
using WifiScannerPedwar.Services;
using WifiScannerLib;
using Microsoft.CodeAnalysis;

using SDD = System.Diagnostics.Debug;
using System.Linq;

namespace WifiScannerPedwar.ViewModels;

public class MainViewModel : ViewModelBase
{
    WifiService WS;
    Avalonia.Threading.DispatcherTimer Amserwr;

    public MainViewModel(IWS? _IWScanner)
    {
        //checks if the inputted IWS isn't null (it is on initialisation/perm requesting)
        if (_IWScanner != null)
        {
            //creates a new wifi service using the selected IWS
            WS = new WifiService(_IWScanner);

            //creates a dispatch timer for scheduling scans
            Amserwr = new Avalonia.Threading.DispatcherTimer();
        
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
        else
        {/*something to do while waiting for perms?*/}
    }

    private void Tick(object? sender, System.EventArgs e)
    {
        //calls add items and asks WS to generate new results
        WifiList.AddItems(WS.GetItems());
    }

    //data holder
    public WifiViewModel WifiList { get; }
}
