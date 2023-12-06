using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using Avalonia.Platform.Storage;

using MsBox.Avalonia;

using WifiScannerLib;

using SDD = System.Diagnostics.Debug;

namespace WifiScannerPedwar.Services
{
    //effectively a wrapper?
    public class WifiService
    {
        public static IWS? WScanner;
        public IStorageProvider? Storage = null;

        private Dictionary<string, WifiInfoItem> Data;
        private static int PosCounter = 0;
        private IStorageFile? ISF = null;

        public WifiService(IWS? _WScanner)
        {
            WScanner = _WScanner;

            SDD.WriteLine($"WScanner is {WScanner.GetType()}");

            if (WScanner != null)
            { WScanner.ScanReturned += ScanReturned; }
            else
            { throw new Exception("WScanner was null!"); }
        }

        private void ScanReturned(object? sender, System.EventArgs e)
        {
            SDD.WriteLine("Scan returned!");

            if (e is WifiEvent We)
            { Data = We.Data; }

            if (Data != null)
            { SDD.WriteLine($"There are {Data.Count} APs within range"); }
            else
            { SDD.WriteLine("Data was null!"); }


            SaveData();
        }

        public void TriggerScan()
        {
            SDD.WriteLine("Scan triggered!");
            WScanner.TriggerScan();
        }


        /*
         * Need to work out how to use bookmarked files because I don't think I 
         * can append to a file like this. Further testing needed!
         */
        private Task SaveData()
        {
            SDD.WriteLine("Data saving!");

            return Task.Run(async () =>
            {
                using (StreamWriter Writer = new StreamWriter(await ISF.OpenWriteAsync()))
                {
                    Writer.Write(JsonSerializer.Serialize(Data));
                }
            });

        }

        public async void SetupISF()
        {
            if (Storage == null && (Storage.CanSave && Storage.CanPickFolder))
            {
                ISF = await Storage.SaveFilePickerAsync(new FilePickerSaveOptions
                {
                    DefaultExtension = ".apdat",
                    SuggestedFileName = $"{DateTime.Now.ToString("[yyyy-MM-dd HH-mm]")}",
                    Title = $"Please choose where to save AP Data"
                });
            }
            else
            {
                MessageBoxManager
                    .GetMessageBoxStandard("Storage error", "Could not access storage")
                    .ShowAsync();
            }
        }
    }
}
