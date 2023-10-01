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

    public MainViewModel()
    {
        var Data = WS.GetItems();

        SDD.WriteLine($"**** Data contains {Data.Count()} elements ****");

        WifiList = new WifiViewModel(Data);

        //SDD.WriteLine("Item's gought");
    }

    public WifiViewModel WifiList { get; }
}
