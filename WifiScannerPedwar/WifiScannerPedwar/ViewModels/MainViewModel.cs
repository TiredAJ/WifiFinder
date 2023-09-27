using System.Collections.Generic;
using WifiScannerPedwar.Services;
using WifiScannerLib;

namespace WifiScannerPedwar.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        var Service = new WifiService();

        WifiList = new WifiViewModel(Service.GetItems());
    }

    public WifiViewModel WifiList { get; }
}
