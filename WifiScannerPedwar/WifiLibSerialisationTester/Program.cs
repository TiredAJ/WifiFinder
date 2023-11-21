using System.Text.Json;
using WifiScannerLib;

internal class Program
{
    private static List<WifiInfoItem> Wifis = new List<WifiInfoItem>()
    {
        new WifiInfoItem() { BSSID = "af23a23b", SSID="Glide", Capabilities="idk", Distance="20", PrimaryFrequency=2412, RSSI="-34dbm"},
        new WifiInfoItem() { BSSID = "ab45cd23", SSID="Glide", Capabilities="idk", Distance="25", PrimaryFrequency=2412, RSSI="-40dbm"},
        new WifiInfoItem() { BSSID = "abfc23c3", SSID="Hidden", Capabilities="idk", Distance="22", PrimaryFrequency=5321, RSSI="-54dbm"},
    };

    private static void Main(string[] args)
    {

        string FileLoc = "../Data.json";

        JsonSerializerOptions JSO = new JsonSerializerOptions()
        { AllowTrailingCommas = false, IncludeFields = true, WriteIndented = true, PreferredObjectCreationHandling = System.Text.Json.Serialization.JsonObjectCreationHandling.Replace };


        using (StreamWriter Writer = new StreamWriter(File.Create(FileLoc)))
        {
            //foreach (var W in Wifis)
            //{ Writer.WriteLine(JsonSerializer.Serialize<WifiInfoItem>(W, JSO)); }

            Writer.WriteLine(JsonSerializer.Serialize<List<WifiInfoItem>>(Wifis, JSO));
        }

        List<WifiInfoItem> NewWifis = new List<WifiInfoItem>();


        using (StreamReader Reader = new StreamReader(File.OpenRead(FileLoc)))
        {
            //while (!Reader.EndOfStream)
            //{
            //    string? Temp = Reader.ReadLine();

            //    if (Temp != null)
            //    { NewWifis.Add(JsonSerializer.Deserialize<WifiInfoItem>(Temp, JSO)); }
            //}

            NewWifis = JsonSerializer.Deserialize<List<WifiInfoItem>>(Reader.ReadToEnd(), JSO);
        }

        //NewWifis = NewWifis.OrderBy(X => X.BSSID).ToList();

        foreach (var W in NewWifis)
        { Console.WriteLine(W); }
    }
}