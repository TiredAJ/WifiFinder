using System.Text.Json;
using System.Text.Json.Nodes;

using WifiScannerLib;

namespace DataSorter
{
    class Program
    {
        static List<List<SnapshotData>> Data = new List<List<SnapshotData>>();

        static void Main(string[] args)
        {
            DataLoader();
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

                    Data.Add(TempList);
                    TempList.Clear();
                }
            }

            Console.WriteLine(Data.Count());
        }


    }
}
