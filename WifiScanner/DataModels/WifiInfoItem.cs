#if ANDROID
using Android.Net.Wifi;
#endif
using System;

namespace WifiScanner.DataModels
{

    public class WifiInfoItem
    {
        #if ANDROID
        public WifiInfoItem(ScanResult _SR)
        {
            SSID = _SR.WifiSsid.ToString();
            BSSID = _SR.Bssid;
            RSSI = _SR.Level;
            LastUpdated = TimeSpan.FromMicroseconds(_SR.Timestamp);
        }
    #endif

        public string BSSID { get; set; } = string.Empty;
        public string SSID { get; set; } = string.Empty;
        public float RSSI { get; set; } = -101;
        public string Capabilities { get; set; } = string.Empty;
        public TimeSpan LastUpdated { get; set; } = TimeSpan.Zero;
    }
}