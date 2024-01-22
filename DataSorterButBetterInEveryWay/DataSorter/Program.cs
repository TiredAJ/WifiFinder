// Ignore Spelling: BSSID RSSI SSID

using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;

using WifiScannerLib;

namespace DataSorter;

class Program
{
    static List<List<SnapshotData>> InitData = new List<List<SnapshotData>>();
    static Dictionary<string, FlattenedData> Data = new Dictionary<string, FlattenedData>();
    //wtf was the string for? AP - AP BSSID?

    public static int CollidingBSSIDs = 0;
    public static int APCount = 0;
    static void Main(string[] args)
    {
        DataLoader();

        Dictionary<int, FlattenedData> Data = new();

        foreach (var Item in SortData())
        {
            /*Data.Add(Item.Key,*/
            CompressSnapshots(Item.Value); //);
        }

        Console.WriteLine($"\n\nBSSID Collisions: {CollidingBSSIDs}");
        Console.WriteLine($"Total APs: {APCount}");
        //SortData();
        //OrderAPs();

        //while (true)
        //{
        //    int Temp = int.Parse(Console.ReadLine());
        //    Console.WriteLine(WifiInfoItem.GetLevel(Temp));
        //}

        //Console.WriteLine("38:90:a5:b1:00:25".HexToInt());
    }

    static void DataLoader()
    {
        List<string> FilePaths = Directory.GetFiles
            (Environment.CurrentDirectory).ToList();

        foreach (string FilePath in FilePaths)
        {
            using (StreamReader JStream = new StreamReader(FilePath))
            {
                var Temp = JsonSerializer
                                        .Deserialize<List<SnapshotData>>
                                        (JStream.BaseStream);

                InitData.Add(Temp);


                //var JNode = JsonNode.Parse
                //    (
                //        JStream.ReadToEnd(),
                //        new JsonNodeOptions() { PropertyNameCaseInsensitive = false },
                //        new JsonDocumentOptions() { AllowTrailingCommas = true }
                //    );

                //List<SnapshotData> TempList = new List<SnapshotData>();

                //foreach (var JN in JNode.AsArray())
                //{ TempList.Add(new SnapshotData(JN)); }

                //InitData.Add(TempList);
            }
        }
    }

    static Dictionary<int, List<SnapshotData>> SortData()
    {
        Dictionary<int, List<SnapshotData>> Intermediate = new();
        //         ^Node       ^Data
        //               ^Repeats

        foreach (var File in InitData)
        {
            foreach (var Snap in File)
            {
                if (Intermediate.ContainsKey(Snap.Index))
                { Intermediate[Snap.Index].Add(Snap); }
                else
                { Intermediate.Add(Snap.Index, new() { Snap }); }
            }
        }

        foreach (var Snaps in Intermediate.Values)
        {
            foreach (var Data in Snaps)
            {
                Data.Data = Data.Data
                            .OrderBy(X => X.Value.BSSID)
                            .ToDictionary
                            (X => X.Key,
                            Y => Y.Value);
            }
        }

        //Intermediate holds the data for each node in the corridor

        return Intermediate;

        Console.WriteLine("Donezo");
    }

    private static List<FlattenedData> CompressSnapshots(List<SnapshotData> _Data)
    {
        Dictionary<string, List<WifiInfoItem>> APs = new();

        //Gets all the APs and collects them by 
        foreach (var Snap in _Data)
        {
            foreach (var AP in Snap.Data)
            {
                if (APs.ContainsKey(AP.Key))
                { APs[AP.Key].Add(AP.Value); }
                else
                { APs.Add(AP.Key, new() { AP.Value }); }
            }
        }

        //filters out APs that:
        // - Are below -80dbm RSSI
        // - Appear less than 3 times
        // - have varying SSIDs
        // - Are named "Hydra5GHz", not really sure atm what they are
        //      but they're causing a lot of BSSID collisions
        foreach (var AP in APs)
        {
            AP.Value.RemoveAll(X => X.SSID == "Hydra5GHz");

            if (AP.Value.Count() < 2)
            {
                APs.Remove(AP.Key);
                continue;
            }

            if (AP.Value.Select(X => X._RSSI).Average() < -80)
            { APs.Remove(AP.Key); }

            if (AP.Value.Select(X => X.SSID).GroupBy(X => X).Count() > 1)
            {
                Debug.WriteLine($"\nColliders: {AP.Key}");

                foreach (var WII in AP.Value)
                { Debug.Write($"\t{WII.SSID}, "); }

                CollidingBSSIDs++;

                APs.Remove(AP.Key);
            }

            if (AP.Value.Select(X => X.PrimaryFrequency).GroupBy(X => X).Count() > 1)
            {
                //APs[AP.Key] = AP.Value
                //                    .Select(X => X.PrimaryFrequency)
                //                    .GroupBy(X => X)
                //                    .OrderByDescending(X => X)
                //                    .First();

                //Because we're basically just using distance at this point
                //What if we store the average "deviance" or range between
                //the distance values?
                //So if it's like 10% difference between the max value and the median
                //and 11% between the minimum and the median, call it 10.5% deviation
                //and store that too?
            }

            APCount++;
        }


        //At this point, we have a collection of APs for a node in a corridor
        float Max, Min, Median = 0;
        IEnumerable<float> Distances;// = new();

        foreach (var Node in APs)
        {
            FlattenedData Temp = new()
            { BSSID = Node.Key, SSID = Node.Value.First().SSID };

            Temp.Distance = Node.Value
                            .Select(X => X._Distance)
                            .Average();

            Distances = Node.Value
                        .Select(X => X._Distance);

            Max = Distances.Max();
            Min = Distances.Min();

            if (Distances.Count() == 2)
            { Temp.DistanceDeviance = (int)(Max - Min); }
            else
            {
                Median = Distances
                            .OrderByDescending(X => X)
                            .ElementAt((int)Math.Floor((double)Distances.Count() / 2d));

                Temp.DistanceDeviance = (int)((Max - Median) + (Median - Min)) / 2;
            }
        }

        return new();


        //throw new NotImplementedException();
    }

    /*private static FlattenedData CompressAPs(KeyValuePair<string, List<WifiInfoItem>> _AP)
    {


        throw new NotImplementedException();
    }

    private static void PushData(
        Dictionary<int, Dictionary<string, List<FlattenedData>>> _Intermediate,
        SnapshotData _Snap)
    {
        foreach (var KVP in _Snap.Data)
        {
            if (_Intermediate[_Snap.Index].ContainsKey(KVP.Key))
            { _Intermediate[_Snap.Index][KVP.Key].Add(new FlattenedData(KVP.Value)); }
            else
            {
                _Intermediate[_Snap.Index].Add(KVP.Key,
                    new List<FlattenedData>()
                        { new FlattenedData(KVP.Value) });
            }
        }
    }*/

    private void SaveData()
    {
        string Temp = Path.Combine(Environment.CurrentDirectory, "Summed");

        //using (StreamWriter Writer = new StreamWriter())
        //{
        //
        //}
    }

    private static void OrderAPs()
    {
        List<string> FilePaths = Directory.GetFiles
            (Environment.CurrentDirectory).ToList();

        foreach (string FilePath in FilePaths)
        {
            List<SnapshotData> TempList;

            using (StreamReader JStream = new StreamReader(FilePath))
            {
                var JNode = JsonNode.Parse
                    (
                        JStream.ReadToEnd(),
                        new JsonNodeOptions() { PropertyNameCaseInsensitive = false },
                        new JsonDocumentOptions() { AllowTrailingCommas = true }
                    );

                TempList = new List<SnapshotData>();

                SnapshotData SD;

                foreach (var JN in JNode.AsArray())
                {
                    SD = new SnapshotData(JN);

                    SD.Data = SD.Data
                        //.OrderByDescending(X => X.Value._RSSI)
                        .OrderByDescending(X => X.Value.BSSID)
                        .ToDictionary(X => X.Key, Y => Y.Value);

                    TempList.Add(SD);
                }
            }

            using (StreamWriter Writer = new StreamWriter(FilePath, false))
            {
                Writer.Write(JsonSerializer.Serialize
                (TempList, new JsonSerializerOptions()
                { WriteIndented = true, IncludeFields = true }));
            }
        }
    }
}

public struct FlattenedData
{
    //public int RSSI { get; set; } = 1;
    public float Distance { get; set; } = -1;
    //public float PrimaryFrequency { get; set; } = -1;
    public int DistanceDeviance { get; set; } = 0;
    public string BSSID { get; set; } = string.Empty;
    public string SSID { get; set; } = string.Empty;

    //what if it doesn't store the RSSI? (or PF)
    //hear me out
    //what if the AP changes it's primary frequency (PF) due to traffic etc.
    //then the rssi is gonna change and nothing matches
    //but also the BSSID has a chance to change perhaps. Lower risk?

    public FlattenedData()
    { }

    public FlattenedData(/*int _RSSI,*/ float _Distance)
    {
        /*RSSI = _RSSI;*/
        Distance = _Distance;
    }

    public FlattenedData(WifiInfoItem _WII)
    {
        /*RSSI = _WII._RSSI;*/
        Distance = _WII._Distance;
        /*PrimaryFrequency = _WII.PrimaryFrequency;*/
        BSSID = _WII.BSSID;
        SSID = _WII.SSID;
    }
}

public static class Extensions
{
    public static Int128 HexToInt(this string _In)
    {
        _In = _In.Replace(":", "");

        return Int128.Parse(_In, NumberStyles.HexNumber);
    }
}

