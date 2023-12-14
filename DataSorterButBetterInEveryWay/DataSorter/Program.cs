using System.Text.Json;
using System.Text.Json.Nodes;

using WifiScannerLib;

namespace DataSorter
{
    class Program
    {
        static List<List<SnapshotData>> InitData = new List<List<SnapshotData>>();
        static Dictionary<string, FlattenedData> Data = new Dictionary<string, FlattenedData>();

        static void Main(string[] args)
        {
            DataLoader();

            FlattenData();
        }

        static void DataLoader()
        {
            List<string> FilePaths = Directory.GetFiles
                (Environment.CurrentDirectory).ToList();

            foreach (string FilePath in FilePaths)
            {
                using (StreamReader JStream = new StreamReader(FilePath))
                {
                    var JNode = JsonNode.Parse
                        (
                            JStream.ReadToEnd(),
                            new JsonNodeOptions() { PropertyNameCaseInsensitive = false },
                            new JsonDocumentOptions() { AllowTrailingCommas = true }
                        );

                    List<SnapshotData> TempList = new List<SnapshotData>();

                    foreach (var JN in JNode.AsArray())
                    { TempList.Add(new SnapshotData(JN)); }

                    InitData.Add(TempList);
                }
            }

            Console.WriteLine(InitData.Count());
        }

        static void FlattenData()
        {
            Dictionary<int, Dictionary<string, List<FlattenedData>>> Intermediate = new Dictionary<int, Dictionary<string, List<FlattenedData>>>();

            //you fucking idiot, it needs to be flattened to per node,
            //not per AP

            foreach (var ID in InitData)
            {//ID is a file

                foreach (var Snap in ID)
                {//each snapshot in a file

                    if (Intermediate.ContainsKey(Snap.Index))
                    {//check if Intermediate has this index

                        PushData(Intermediate, Snap);
                    }
                    else
                    {
                        Intermediate.Add(Snap.Index, new Dictionary<string, List<FlattenedData>>());
                        PushData(Intermediate, Snap);
                    }
                }
            }

            //foreach (var I in Intermediate)
            //{ Data.Add(I.Key, I.Value.Average()); }
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
    }
}
