using System;
using Avalonia.Platform.Storage;
using System.Collections.Generic;
using WifiScannerLib;

namespace WifiScannerPedwar.Services
{
    //effectively a wrapper?
    public class WifiService
    {
        public static IWS? WScanner;
        public static IStorageProvider? Storage = null;


        private Dictionary<string, WifiInfoItem> Data;
        private static int PosCounter = 0;

        public WifiService(IWS? _WScanner)
        {
            WScanner = _WScanner;

            if (WScanner != null)
            {WScanner.ScanReturned += ScanReturned;}
            else
            {throw new Exception("WScanner was null!");}
        }

        private void ScanReturned(object? sender, System.EventArgs e)
        {
            if (e is WifiEvent We)
            { var Data = We.Data; }

            SaveData();
        }

        public static void TriggerScan()
        { WScanner.TriggerScan(); }

        private void SaveData()
        {
            if (Storage == null)
            {System.Console.WriteLine("Storage was null!");}
            else
            {System.Console.WriteLine("Storage wasn't null");}
        }
    }

    //Potential options
    /*
     * - Session file vs file-per-scan
     */
}
