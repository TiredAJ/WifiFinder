using System;
using System.Linq;
using System.Collections.Generic;
#if ANDROID
using Android.Net.Wifi;
using Android.Content;
using Android.App;
using Android.OS;
using Android.Net;
using Android;
using SDD =  System.Diagnostics.Debug;
#endif

namespace WifiScanner.DataModels
{
#if ANDROID
    [Service(Exported =true)]
    public class WifiDataService: Service
    {
        private WifiManager? WM;
        private ConnectivityManager? CM;
        private NC NetCB { get; set; }

        private bool IsRegistered = false;

        private List<WifiInfoItem> ScanData = new List<WifiInfoItem>();

        public override void OnCreate()
        {            
            Context temp = Android.App.Application.Context;

            if (temp == null)
            {throw new NullReferenceException("Temp was null!?");}

            SDD.Write($"access wifi state: {temp.CheckSelfPermission(Manifest.Permission.AccessWifiState)}");
            SDD.Write($"access fine location: {temp.CheckSelfPermission(Manifest.Permission.AccessFineLocation)}");
            SDD.Write($"change wifi state: {temp.CheckSelfPermission(Manifest.Permission.ChangeWifiState)}");

            WM = (WifiManager)temp.GetSystemService(WifiService);
            CM = (ConnectivityManager)temp.GetSystemService(ConnectivityService);

            if (WM == null)
            {throw new Exception("WM was null!");}

            NetCB = new NC();

            NetCB.Initialise(CB);
        }

        //is called to collect data
        public List<WifiInfoItem> GetData()
        {
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

            ////get data
            //if (WM != null)
            //{WM.StartScan();}

            return ScanData;
        }

        //collects the data
        public bool CB()
        {
            ScanData.Clear();

            if (WM.ScanResults.Count > 0)
            {
                foreach (var N in WM.ScanResults)
                {ScanData.Add(new WifiInfoItem(N));}

                return true;
            }
            else
            {return false;}
        }

        //only here because Service inheritance mandates it
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
#endif
}
