using System.Collections.Generic;
using WifiScannerPedwar.Services;
using WifiScannerLib;
using Microsoft.CodeAnalysis;

using SDD = System.Diagnostics.Debug;

namespace WifiScannerPedwar.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        var Service = new WifiService();

        WifiList = new WifiViewModel(Service.GetItems());

        //SDD.WriteLine("Item's gought");
    }

    public WifiViewModel WifiList { get; }
}
