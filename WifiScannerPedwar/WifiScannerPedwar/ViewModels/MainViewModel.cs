using WifiScannerLib;
using WifiScannerPedwar.Services;
using SDD = System.Diagnostics.Debug;

namespace WifiScannerPedwar.ViewModels;

public class MainViewModel : ViewModelBase
{
    private static WifiService WS;

    public MainViewModel(IWS? _IWScanner)
    {
        //checks if the inputted IWS isn't null (it is on initialisation/perm requesting)
        if (_IWScanner != null)
        { Initialise(_IWScanner); }
        else
        {/*something to do while waiting for perms?*/}
    }

    public static void Initialise(IWS _IWScanner)
    {
        //creates a new wifi service using the selected IWS
        WS = new WifiService(_IWScanner);


        //initial data nabbing
        var Data = WS.GetItems();



        //generates new data holder
        WifiList = new WifiViewModel(Data);

        //temp
        SDD.WriteLine("Items got");
    }

    public

    //data holder
    public static WifiViewModel WifiList
    { get; private set; } = new WifiViewModel();
}
