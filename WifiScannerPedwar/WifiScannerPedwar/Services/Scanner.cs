using System.Collections.Generic;
using WifiScannerLib;

namespace WifiScannerPedwar.Services
{
    //effectively a wrapper?
    public class WifiService
    {
        public IWS? WScanner;

        Dictionary<string, WifiInfoItem> Data;

        public WifiService(IWS? _WScanner)
        {
            WScanner = _WScanner;

            WScanner.ScanReturned += ScanReturned;
        }

        private void ScanReturned(object? sender, System.EventArgs e)
        {
            if (e is WifiEvent We)
            { var Data = We.Data; }
        }

        public Dictionary<string, WifiInfoItem>? GetItems()
        {
            if (WScanner != null)
            { return Data; }
            else
            { return null; }
        }
    }
}
