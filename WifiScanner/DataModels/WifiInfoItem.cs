using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WifiScanner.DataModels
{
    /// <summary>
    /// ToDoItem
    /// </summary>
    public class WifiInfoItemOld
    {
        public string BSSID { get; set; } = string.Empty;
        public string SSID { get; set; } = string.Empty;
        public float RSSI { get; set; } = -89;
        public double Latitude { get; set; } = 0.0d;
        public double Longitude { get; set; } = 0.0d;
        public float Accuracy { get; set; } = 100f;
        public DateTimeOffset TimeStamp { get; set; } = DateTimeOffset.Now;
    }

    public class WifiInfoItem
    {
        //public WifiInfoItem(BssNetworkPack _BNP)
        //{
        //    SSID = _BNP.Ssid.ToString();
        //    BSSID = _BNP.Bssid.ToString();
        //    Band = _BNP.Band;
        //    Frequency = _BNP.Frequency;
        //    Channel = _BNP.Channel;
        //    LinkQuality = _BNP.LinkQuality;
        //    RSSI = _BNP.SignalStrength;
        //    LastUpdated = DateTime.Now.TimeOfDay;
        //}

        public string SSID { get; set; }
        public string BSSID { get; set; }
        public int Frequency { get; set; }
        public int Channel { get; set; }
        public int RSSI { get; set; }
        public int LinkQuality { get; set; }
        public float Band { get; set; }
        public TimeSpan LastUpdated { get; set; }
    }

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
}
