﻿using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;

using WifiScannerLib;

namespace DataSorter;

class Program
{
    static List<List<SnapshotData>> InitData = new List<List<SnapshotData>>();
    static Dictionary<string, FlattenedData> Data = new Dictionary<string, FlattenedData>();
    //wtf was the string for?

    static void Main(string[] args)
    {
        DataLoader();

        Dictionary<int, FlattenedData> Data = new();

        foreach (var Item in SortData())
        { Data.Add(Item.Key, CompressSnapshots(Item.Value)); }


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

        Console.WriteLine(InitData.Count());
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

    private static FlattenedData CompressSnapshots(List<SnapshotData> _Data)
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

        //removes APs that are present only once
        foreach (var AP in APs)
        {
            if (AP.Value.Count() < 2)
            { APs.Remove(AP.Key); }
        }





        throw new NotImplementedException();
    }

    private static FlattenedData CompressAPs(KeyValuePair<string, List<WifiInfoItem>> _AP)
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
    }

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
    public int RSSI { get; set; }
    public double Distance { get; set; }

    public FlattenedData(int _RSSI, double _Distance)
    {
        RSSI = _RSSI;
        Distance = _Distance;
    }

    public FlattenedData(WifiInfoItem _WII)
    {
        RSSI = _WII._RSSI;
        Distance = _WII._Distance;
    }
}

public static class Extensions
{
    public static FlattenedData Average(this List<FlattenedData> _Input)
    {
        FlattenedData Temp = new FlattenedData();

        Temp.RSSI = (int)_Input
                        .Select(X => X.RSSI)
                        .Average();

        Temp.Distance = _Input
                        .Select(X => X.Distance)
                        .Average();

        return Temp;
    }

    public static Int128 HexToInt(this string _In)
    {
        _In = _In.Replace(":", "");

        return Int128.Parse(_In, NumberStyles.HexNumber);
    }
}

