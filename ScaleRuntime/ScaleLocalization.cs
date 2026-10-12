using System;
using System.Collections.Generic;
using System.Text;

namespace MPatcherFork.ScaleRuntime
{
    // Keep state and logs in English. Translate only at the display boundary,
    // using the game's selected language, including already cached statuses.
    internal static class ScaleLocalization
    {
        private static readonly Dictionary<string, string> japanese = new Dictionary<string, string>(StringComparer.Ordinal);
        private static List<string> longestFirst;
        private static readonly Dictionary<string, string> displayCache = new Dictionary<string, string>(StringComparer.Ordinal);

        static ScaleLocalization()
        {
            // The stable core reports these probe errors in English. Localize
            // their displayed reason here without linking the core to the UI.
            Source("Visual link calculation is not registered.", "表示用の接続計算が登録されていません。");
            Source("Nested visual pose calculations are not allowed.", "表示姿勢の計算を入れ子にすることはできません。");
            Source("Visual pose scopes must close in reverse order.", "表示姿勢の計算範囲は、開始と逆の順序で閉じる必要があります。");
            Source("Native visual pose CheckLinkG", "標準の表示姿勢の CheckLinkG");
        }

        internal static string Source(string english, string translation)
        {
            if (!japanese.ContainsKey(english))
            {
                japanese.Add(english, translation);
                longestFirst = null;
                displayCache.Clear();
            }
            return english;
        }

        internal static string Display(string source)
        {
            return Display(source, HelpDefs.isJ);
        }

        internal static string Display(string source, bool isJapanese)
        {
            if (!isJapanese || string.IsNullOrEmpty(source)) return source ?? string.Empty;
            string cached;
            if (displayCache.TryGetValue(source, out cached)) return cached;
            if (longestFirst == null)
            {
                longestFirst = new List<string>(japanese.Keys);
                longestFirst.Sort(delegate(string a, string b) { return b.Length.CompareTo(a.Length); });
            }
            // Translate original fragments exactly once. Dynamic numbers and
            // native exception details remain intact; translated text is never
            // processed again as a potential English source fragment.
            StringBuilder result = new StringBuilder();
            for (int offset = 0; offset < source.Length;)
            {
                string match = null;
                foreach (string key in longestFirst)
                    if (key.Length <= source.Length - offset
                        && string.CompareOrdinal(source, offset, key, 0, key.Length) == 0)
                    { match = key; break; }
                if (match == null) result.Append(source[offset++]);
                else { result.Append(japanese[match]); offset += match.Length; }
            }
            string localized = result.ToString();
            if (displayCache.Count >= 64) displayCache.Clear();
            displayCache[source] = localized;
            return localized;
        }
    }
}
