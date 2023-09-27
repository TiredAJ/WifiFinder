﻿#if ANDROID
using Android.Net.Wifi;
#elif IOS
using NetworkExtension;
#endif

namespace WifiScannerLib
{
    // All the code in this file is included in all platforms.

    public interface IWS
    {public List<WifiInfoItem> GetData();}


    public class WifiInfoItem
    {
        #region Android
#if ANDROID
        public WifiInfoItem(ScanResult _SR)
        {
#if ANDROID33_0_OR_GREATER
            SSID = _SR.WifiSsid.ToString();
#elif ANDROID21_0_OR_GREATER
            SSID = _SR.Ssid;
#endif
            BSSID = _SR.Bssid;
            RSSI = _SR.Level;
            LastUpdated = TimeSpan.FromMicroseconds(_SR.Timestamp);
        }
#endif
        #endregion

        #region IOS
#if IOS
        public WifiInfoItem(NetworkExtension.NEHotspotHelperResult NEHHR)
        {
        }
#endif
        #endregion

        public WifiInfoItem()
        { }

        public string BSSID { get; set; } = string.Empty;
        public string SSID { get; set; } = string.Empty;
        public float RSSI { get; set; } = -101;
        public string Capabilities { get; set; } = string.Empty;
        public TimeSpan LastUpdated { get; set; } = TimeSpan.Zero;
    }
}