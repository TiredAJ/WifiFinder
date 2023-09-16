using System;
using System.Linq;
using System.Collections.Generic;
using ManagedNativeWifi;

namespace WifiScanner.DataModels
{
    public class WifiData
    {
        //public WifiData()
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

        public static List<WifiInfoItem> GetData()
        {
            List<WifiInfoItem> Temp = new List<WifiInfoItem>();

            IEnumerable<BssNetworkPack> BNP = NativeWifi.EnumerateBssNetworks();

            foreach (var Item in BNP)
            {Temp.Add(new WifiInfoItem(Item));}

            return Temp;
        }

    }
}
