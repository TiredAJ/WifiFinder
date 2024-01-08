// Ignore Spelling: IWS Wifi RSSI SSID BSSID NEHHR

using WifiScannerLib;

namespace DataSorter;

public class WifiModel
{
    public string BSSID { get; set; } = string.Empty;
    public string SSID { get; set; } = string.Empty;
    public int _RSSI { get; set; } = -101;
    public string Capabilities { get; set; } = string.Empty;
    public TimeSpan LastUpdated { get; set; } = TimeSpan.Zero;
    public double _Distance { get; set; } = 0d;
    public double PrimaryFrequency { get; set; } = 0d;
}

public record SnapshotData
{
    public int Index { get; set; } = 0;
    public TimeSpan LastUpdated { get; set; }
    public Dictionary<string, WifiInfoItem> Data { get; set; }
        = new();
}
