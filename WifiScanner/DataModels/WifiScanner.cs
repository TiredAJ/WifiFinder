using System;
using System.Linq;
using System.Collections.Generic;
using Android.Net.Wifi;
using Android.Content;
using Android.App;
using Android.OS;
using Android.Net;
using Android;

//using ANW = Android.Net.Wifi;
using SDD =  System.Diagnostics.Debug;

namespace WifiScanner.DataModels
{
    [Service(Exported =true)]
    public class WifiDataService: Service
    {
        /*public WifiDataService()
        {
            var NetworkData = NativeWifi.EnumerateAvailableNetworks();

            NetworkData.FirstOrDefault();


            var BssData = NativeWifi.EnumerateBssNetworks();

            BssData.FirstOrDefault();

            //NetworkIdentifier Temp = BssData.First().Ssid;

            //Temp.

            //What we have access to:
            //ND
            //  SSID    NetworkIdentifier
            //  BSSType     BSSType
            //  ProfileName     String
            //  SignalQuality   int (0-100)
            //BD
            //  Frequency   int
            //  Channel     int
            //  BSSID       NetworkIdentifier
            //  Band        float
            //  SignalStrength (RSSI)   int
            //  LinkQuality     int (0-100)

        }*/

        public static WifiManager? WM;

        private ConnectivityManager? CM;
        private NC NetCB { get; set; }

        private bool IsRegistered = false;

        public override void OnCreate()
        {            
            Context temp = Android.App.Application.Context;

            SDD.Write($"access wifi state: {temp.CheckSelfPermission(Manifest.Permission.AccessWifiState)}");
            SDD.Write($"access fine location: {temp.CheckSelfPermission(Manifest.Permission.AccessFineLocation)}");
            SDD.Write($"change wifi state: {temp.CheckSelfPermission(Manifest.Permission.ChangeWifiState)}");

            WM = (WifiManager)temp.GetSystemService(WifiService);
            CM = (ConnectivityManager)temp.GetSystemService(ConnectivityService);

            if (WM == null)
            {throw new Exception("WM was null!");}

            NetCB = new NC();

            NetCB.Initialise(CB);

            Scan();
        }

        //temp
        public void Scan()
        {
            SDD.WriteLine("Rescanning");

            if (IsRegistered)
            {
                CM.UnregisterNetworkCallback(NetCB);
                IsRegistered = false;
            }

            if (!IsRegistered)
            {
                CM.RegisterNetworkCallback
                (
                    new NetworkRequest.Builder().AddTransportType(TransportType.Wifi).Build(),
                    NetCB
                );
                IsRegistered = true;
            }
        }

        //is called to collect data
        public List<WifiInfoItem> GetData()
        {
            //get data
            if (WM != null)
            {
                WM.StartScan();
            }


            return new List<WifiInfoItem>();
        }

        //collects the data
        public bool CB()
        {
            if (WM.ScanResults.Count > 0)
            {
                foreach (var N in WM.ScanResults)
                {SDD.WriteLine($"{N.Ssid} [{N.Level}]");}

                return true;
            }
            else
            {return false;}
        }

        public override IBinder? OnBind(Intent? intent)
        {throw new NotImplementedException();}
    }

    public class NC : ConnectivityManager.NetworkCallback
    {
        public Func<bool> CallBack { get; set; }

        public void Initialise(Func<bool> _CallBack)
        {CallBack = _CallBack;}

        public NC()
        { }

        public override void OnAvailable(Network network)
        {CallBack();}
    }

}
