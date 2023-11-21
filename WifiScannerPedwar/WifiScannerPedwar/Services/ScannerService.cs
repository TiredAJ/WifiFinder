using Avalonia.Platform.Storage;
using System.Collections.Generic;
using WifiScannerLib;

namespace WifiScannerPedwar.Services
{
    //effectively a wrapper?
    public class WifiService
    {
        public IWS? WScanner;
        public static IStorageProvider? Storage = null;


        private Dictionary<string, WifiInfoItem> Data;
        private static int PosCounter = 0;

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

        public void TriggerScan()
        { WScanner.TriggerScan(); }

        private void SaveData()
        {

        }
    }

    //Potential options
    /*
     * - Session file vs file-per-scan
     */
}
