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
        if (_IWScanner != null)
        {
            WS = new WifiService(_IWScanner);

            Amserwr = new Avalonia.Threading.DispatcherTimer();
        
            var Data = WS.GetItems();

            Amserwr.Interval = new System.TimeSpan(0, 0, 5);
            Amserwr.Tick += Tick;
            Amserwr.Start();


            //SDD.WriteLine($"**** Data contains {Data.Count()} elements ****");

            WifiList = new WifiViewModel(Data);

            SDD.WriteLine("Item's gought");
        }
        else
        {/*something to do while waiting for perms?*/}
    }

    private void Tick(object? sender, System.EventArgs e)
    {
        WifiList.AddItems(WS.GetItems());
    }

    public WifiViewModel WifiList { get; }
}
