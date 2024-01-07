using System.Collections.Generic;

using WifiScannerLib;

namespace WifiScannerPedwar
{
    public static class Extensions
    {
        public static int InternalCount<Tk, Tv>(this List<Dictionary<Tk, Tv>> _Data)
        {
            if (_Data.Count == 0)
            { return 0; }

            int Count = 0;

            foreach (var Dict in _Data)
            { Count += Dict.Count; }

            return Count;
        }

        public static int InternalCount(this List<SnapshotData> _Data)
        {
            if (_Data.Count == 0)
            { return 0; }

            int Count = 0;

            foreach (var Dict in _Data)
            { Count += Dict.Data.Count; }

            return Count;
        }
    }
}
