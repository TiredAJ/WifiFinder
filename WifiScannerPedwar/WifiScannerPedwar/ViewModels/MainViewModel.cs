using System.Collections.Generic;
using WifiScannerPedwar.Services;
using WifiScannerLib;
using Microsoft.CodeAnalysis;

using SDD = System.Diagnostics.Debug;
using System.Linq;

namespace WifiScannerPedwar.ViewModels;

public class MainViewModel : ViewModelBase
{
    WifiService WS = new WifiService();
    Avalonia.Threading.DispatcherTimer Amserwr;


    public MainViewModel()
    {
        Amserwr = new Avalonia.Threading.DispatcherTimer();
        
        var Data = WS.GetItems();

        Amserwr.Interval = new System.TimeSpan(0, 0, 5);
        Amserwr.Tick += Tick;
        Amserwr.Start();


        SDD.WriteLine($"**** Data contains {Data.Count()} elements ****");

        WifiList = new WifiViewModel(Data);

        //SDD.WriteLine("Item's gought");
    }

    private void Tick(object? sender, System.EventArgs e)
    {
        WifiList.AddItems(WS.GetItems());
    }

    public WifiViewModel WifiList { get; }
}
