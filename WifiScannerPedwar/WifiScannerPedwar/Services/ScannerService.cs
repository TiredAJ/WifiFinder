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
        private List<SnapshotData> Data = new List<SnapshotData>();
        public event EventHandler<APCount> CountUpdated;
        public Func<Dictionary<string, WifiInfoItem>, Dictionary<string, WifiInfoItem>>? Sorter = null;

        public WifiService(IWS? _WScanner)
        {
            WScanner = _WScanner;

            if (WScanner != null)
            { WScanner.ScanReturned += ScanReturned; }
            else
            { throw new Exception("WScanner was null!"); }
        }

        public static bool CheckLocation()
        { throw new NotImplementedException(); }

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

            if (Sorter != null)
            { IntermediateData = Sorter(IntermediateData); }

            Data.Add(new SnapshotData(DateTime.Now.TimeOfDay, IntermediateData));

            CountUpdated?.Invoke(this, new APCount(Data.InternalCount()));
        }

        public void TriggerScan()
        {
            SDD.WriteLine("Scan triggered!");

            if (WScanner != null)
            { WScanner.TriggerScan(); }
            else
            { ErrorBox("Error!", "WScanner was null!"); }
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

                try
                {
                    //ErrorBox("Data", $"{Data.Count} Items written");

                    using (StreamWriter Writer = new StreamWriter(await ISF.OpenWriteAsync()))
                    {
                        await Writer.WriteAsync
                        (JsonSerializer.Serialize(Data, new JsonSerializerOptions() { WriteIndented = true }));
                    }
                }
                catch (Exception EXC)
                { ErrorBox("Error!", EXC.Message); }

                Data.Clear();
            }
            else
            { ErrorBox("Storage error", "Could not access storage"); }

            CountUpdated?.Invoke(this, new APCount(Data.InternalCount()));

            //ErrorBox("Data Cleared", $"{Data.Count} Items");
            return;
        }

        public void ClearData()
        {
            Data.Clear();
            CountUpdated?.Invoke(this, new APCount(Data.InternalCount()));
        }

        private void ErrorBox(string _Title, string _Text)
        {
            MessageBoxManager
                .GetMessageBoxStandard(_Title, _Text)
                .ShowAsync();

            SDD.WriteLine($"MessageBox sent: {_Title}, {_Text}");
        }
    }

    public class APCount : EventArgs
    {
        public int Count { get; set; }

        public APCount(int _Count)
        { Count = _Count; }
    }
}
