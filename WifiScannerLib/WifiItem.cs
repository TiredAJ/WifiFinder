﻿using SDD = System.Diagnostics.Debug;

#if ANDROID
using Android.Net.Wifi;
#elif IOS
using NetworkExtension;
#endif

using Microsoft.Maui.Layouts;
using System.Diagnostics.CodeAnalysis;

namespace WifiScannerLib
{
    // All the code in this file is included in all platforms.

    public interface IWS
    {
        public IEnumerable<WifiInfoItem> GetData();
    }


    public class WifiInfoItem : IEquatable<WifiInfoItem>
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

            _RSSI = _SR.Level;
            LastUpdated = TimeSpan.FromMicroseconds(_SR.Timestamp);

            if (SSID == "")
	        {SSID = "*Hidden*";}
            else
	        {SSID = SSID.Trim('\"', ' ');}
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
        {
        }

        public string BSSID { get; set; } = string.Empty;
        public string SSID { get; set; } = string.Empty;
        private float _RSSI = -101;
        public string RSSI
        {
            get => _RSSI.ToString("#dbm");
            set => _RSSI = float.Parse(value); 
        }
        public string Capabilities { get; set; } = string.Empty;
        public TimeSpan LastUpdated { get; set; } = TimeSpan.Zero;

        public WifiInfoItem Clone()
        {return this.MemberwiseClone() as WifiInfoItem;}

        public bool Equals(WifiInfoItem A, WifiInfoItem B)
        {
            if (A.BSSID == B.BSSID)
            {return true;}
            else
            {return false;}
        }

        public bool Equals(WifiInfoItem B)
        {
            if (this.BSSID == B.BSSID)
            {return true;}
            else
            {return false;}
        }
    }
}