using System;
using System.Linq;
using System.Collections.Generic;
using Android.Net.Wifi;
using System.Diagnostics;
using Android.Content;
using ANW = Android.Net.Wifi;
using Android.App;
using Android.OS;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Net.WebSockets;
using Org.Apache.Http.Conn.Routing;
using Java.Util.Concurrent;
using System.Threading.Tasks;
using Android.Net;
using Android.Content.PM;


namespace WifiScanner.DataModels
{
    //[Service(Exported =true)]
    //[IntentFilter(new[] {START_SCAN, STOP_SCAN})]
    [Activity]
    public class WifiDataService : Service
    {
        public const string START_SCAN = "com.TiredAJ.WifiScanner.START_SCAN";
        public const string STOP_SCAN = "com.TiredAJ.WifiScanner.STOP_SCAN";

        //public WifiDataService()
        //{
        //    var NetworkData = NativeWifi.EnumerateAvailableNetworks();

        //    NetworkData.FirstOrDefault();


        //    var BssData = NativeWifi.EnumerateBssNetworks();

        //    BssData.FirstOrDefault();

        //    //NetworkIdentifier Temp = BssData.First().Ssid;

        //    //Temp.

        //    //What we have access to:
        //    //ND
        //    //  SSID    NetworkIdentifier
        //    //  BSSType     BSSType
        //    //  ProfileName     String
        //    //  SignalQuality   int (0-100)
        //    //BD
        //    //  Frequency   int
        //    //  Channel     int
        //    //  BSSID       NetworkIdentifier
        //    //  Band        float
        //    //  SignalStrength (RSSI)   int
        //    //  LinkQuality     int (0-100)

        //}

        WifiManager? WM;

        ConnectivityManager? CM;

        public WifiManager.ScanResultsCallback ScanResultsCallback { get; set; }

        private NC NetCB { get; set; }

        private IExecutorService EXC = Executors.NewSingleThreadExecutor();

        public override void OnCreate()
        {
            base.OnCreate();
            
            Context temp = Android.App.Application.Context;

            WM = (WifiManager)temp.GetSystemService(WifiService);
            CM = (ConnectivityManager)temp.GetSystemService(ConnectivityService);

            if (WM == null)
            {throw new Exception("WM was null!");}

            NetCB = new NC();

            CM.RegisterNetworkCallback
            (
                new NetworkRequest.Builder().AddTransportType(TransportType.Wifi).Build(),
                NetCB
            );

            ScanResultsCallback = new SRC(CB);

            WM.RegisterScanResultsCallback(EXC, ScanResultsCallback);

            WM.RegisterScanResultsCallback(Executors.NewSingleThreadScheduledExecutor(), ScanResultsCallback);

            WM.StartScan();

            //Task.Run(() =>
            //{
            //    while (true)
            //    {
            //        if (WM.ScanResults.Count > 0)
            //        {System.Diagnostics.Debug.WriteLine(WM.ScanResults.Count);}
            //    }
            //});

            System.Diagnostics.Debug.WriteLine($"Scan Results: {WM.ScanResults.Count}");
        }

        public static Intent CreateIntent(Context _Context, string _Action)
        {
            var _Intent = new Intent(_Context, typeof(WifiDataService));
            _Intent.SetAction(_Action);
            return _Intent;
        }

        public List<WifiInfoItem> GetData()
        {
            //get data
            if (WM != null)
            {
                WM.StartScan();
            }


            return new List<WifiInfoItem>();
        }

        public override IBinder? OnBind(Intent? intent)
        {
            throw new NotImplementedException();
        }

        public bool CB()
        {
            if (WM.ScanResults.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine($"Results: {WM.ScanResults.FirstOrDefault().Ssid}");
                return true;
            }
            else
            {return false;}
        }
        public class BRec : BroadcastReceiver
        {
            public override void OnReceive(Context? context, Intent? intent)
            {
                if (intent.Action == WifiManager.ScanResultsAvailableAction)
                {
                    System.Diagnostics.Debug.WriteLine($"BRec: {}");
                }
            }
        }
    }

    public class SRC : WifiManager.ScanResultsCallback
    {
        public Func<bool> CallBack { get; set; }

        public SRC(Func<bool> _CallBack)
        {CallBack = _CallBack;}

        public override void OnScanResultsAvailable()
        {
            //System.Diagnostics.Debug.WriteLine("Results available------------------");

            CallBack();
        }
    }
    public class NC : ConnectivityManager.NetworkCallback
    {
        public Func<bool> CallBack { get; set; }
        public int Flags { get; set; }

        public void Initialise(Func<bool> _CallBack)
        {CallBack = _CallBack;}

        public NC()
        { }

        public NC(int _Flags)
        {Flags = _Flags;}

        public override void OnAvailable(Network network)
        {
            System.Diagnostics.Debug.WriteLine(network.GetByName);

            CallBack();
        }
    }

}
