using Avalonia.Platform.Storage;

using MsBox.Avalonia;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using WifiScannerLib;

using SDD = System.Diagnostics.Debug;

namespace WifiScannerPedwar.Services
{
    //effectively a wrapper?
    public class WifiService
    {
        public static IWS? WScanner;
        private List<Dictionary<string, WifiInfoItem>> Data = new List<Dictionary<string, WifiInfoItem>>();

        public WifiService(IWS? _WScanner)
        {
            WScanner = _WScanner;

            SDD.WriteLine($"WScanner is {WScanner.GetType()}");

            if (WScanner != null)
            { WScanner.ScanReturned += ScanReturned; }
            else
            { throw new Exception("WScanner was null!"); }
        }

        public static bool CheckLocation()
        {
        }

        private void ScanReturned(object? sender, System.EventArgs e)
        {
            Dictionary<string, WifiInfoItem>? IntermediateData = null;

            SDD.WriteLine("Scan returned!");

            if (e is WifiEvent We)
            { IntermediateData = We.Data; }

            if (IntermediateData != null)
            { SDD.WriteLine($"There are {IntermediateData.Count} APs within range"); }
            else
            { SDD.WriteLine("Data was null!"); return; }

            Data.Add(IntermediateData);
        }

        public void TriggerScan()
        {
            SDD.WriteLine("Scan triggered!");
            WScanner.TriggerScan();
        }

        public async Task SaveToFile(IStorageProvider Storage)
        {
            if (Storage != null && (Storage.CanSave && Storage.CanPickFolder))
            {
                IStorageFile? ISF = await Storage.SaveFilePickerAsync(new FilePickerSaveOptions
                {
                    DefaultExtension = ".apdat",
                    SuggestedFileName = $"{DateTime.Now.ToString("[yyyy-MM-dd HH-mm]")}",
                    Title = $"Please choose where to save AP Data"
                });

                if (ISF == null)
                {
                    ErrorBox("Storage error", "Could not access chosen file");
                    return;
                }

                await Task.Run(async () =>
                {
                    using (StreamWriter Writer = new StreamWriter(await ISF.OpenWriteAsync()))
                    {
                        Writer.Write
                        (JsonSerializer.Serialize(Data, new JsonSerializerOptions() { WriteIndented = true }));
                    }

                    Data.Clear();
                });
            }
            else
            { ErrorBox("Storage error", "Could not access storage"); }
            return;
        }

        private void ErrorBox(string _Title, string _Text)
        {
            MessageBoxManager
                .GetMessageBoxStandard(_Title, _Text)
                .ShowAsync();
        }
    }
}
