using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment5_CSE445F25
{
    // User control that provides a simple NFL player stats "TryIt".
    // It uses an in-memory dictionary and a fuzzy matching routine to find
    // the closest player name to the user's input and display a short summary.
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Intentionally empty: no initialization needed on every load.
            // If you later add first-load initialization, protect it with if (!IsPostBack) { ... }
        }

        // Click handler for the "Lookup Stats" button.
        // Flow:
        // 1) Validate input
        // 2) Build a case-insensitive dictionary of sample player stats
        // 3) Use fuzzy matching to find the best key for the user input
        // 4) Display the matched stats or a not-found message
        protected void btnLookup_Click(object sender, EventArgs e)
        {
            string input = txtPlayer.Text?.Trim();
            if (string.IsNullOrWhiteSpace(input))
            {
                // Avoid searching with empty input
                lblStats.Text = "Enter a player name.";
                return;
            }

            // Expanded NFL player stats dictionary (sample summaries)
            // NOTE: StringComparer.OrdinalIgnoreCase => keys are matched case-insensitively.
            // That means "Patrick Mahomes" == "patrick mahomes" for dictionary lookups.
            // NOTE : for now we don’t pull from an API or database, just hardcoded sample data
            // for demonstration using a dictionary.
            Dictionary<string, string> stats = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                // =========================
                // Quarterbacks
                // =========================
                { "patrick mahomes", "QB — 315 passing yards, 3 TD, 0 INT" },
                { "patrick mahomes ii", "QB — 315 passing yards, 3 TD, 0 INT" },
                { "josh allen", "QB — 284 passing yards, 2 TD, 1 INT, 42 rushing yards" },
                { "joe burrow", "QB — 275 passing yards, 2 TD, 1 INT" },
                { "jalen hurts", "QB — 264 passing yards, 2 TD, 1 INT, 55 rushing yards, 1 rush TD" },
                { "tua tagovailoa", "QB — 318 passing yards, 2 TD, 0 INT" },
                { "lamar jackson", "QB — 238 passing yards, 2 TD, 0 INT, 68 rushing yards" },
                { "justin herbert", "QB — 296 passing yards, 2 TD, 0 INT" },
                { "dak prescott", "QB — 287 passing yards, 3 TD, 1 INT" },
                { "cj stroud", "QB — 301 passing yards, 2 TD, 0 INT" },
                { "c.j. stroud", "QB — 301 passing yards, 2 TD, 0 INT" },
                { "brock purdy", "QB — 274 passing yards, 2 TD, 0 INT" },
                { "jared goff", "QB — 285 passing yards, 2 TD, 1 INT" },
                { "trevor lawrence", "QB — 262 passing yards, 2 TD, 0 INT" },
                { "kirk cousins", "QB — 292 passing yards, 2 TD, 1 INT" },
                { "aaron rodgers", "QB — 245 passing yards, 2 TD, 0 INT" },
                { "jordan love", "QB — 248 passing yards, 2 TD, 1 INT" },
                { "matthew stafford", "QB — 301 passing yards, 2 TD, 1 INT" },
                { "geno smith", "QB — 256 passing yards, 2 TD, 0 INT" },
                { "deshaun watson", "QB — 234 passing yards, 2 TD, 1 INT, 29 rushing yards" },
                { "kyler murray", "QB — 228 passing yards, 2 TD, 1 INT, 36 rushing yards" },
                { "baker mayfield", "QB — 241 passing yards, 2 TD, 0 INT" },
                { "russell wilson", "QB — 223 passing yards, 2 TD, 0 INT, 24 rushing yards" },
                { "justin fields", "QB — 214 passing yards, 1 TD, 1 INT, 72 rushing yards" },
                { "daniel jones", "QB — 219 passing yards, 1 TD, 1 INT, 39 rushing yards" },
                { "sam darnold", "QB — 210 passing yards, 1 TD, 1 INT" },
                { "bryce young", "QB — 198 passing yards, 1 TD, 1 INT" },
                { "mac jones", "QB — 205 passing yards, 1 TD, 1 INT" },

                // =========================
                // Running Backs
                // =========================
                { "christian mccaffrey", "RB — 112 rushing yards, 1 TD, 34 receiving yards" },
                { "derrick henry", "RB — 98 rushing yards, 2 TD, 10 receiving yards" },
                { "saquon barkley", "RB — 87 rushing yards, 1 TD, 21 receiving yards" },
                { "bijan robinson", "RB — 103 rushing yards, 1 TD, 17 receiving yards" },
                { "jonathan taylor", "RB — 94 rushing yards, 1 TD, 15 receiving yards" },
                { "nick chubb", "RB — 88 rushing yards, 0 TD, 12 receiving yards" },
                { "tony pollard", "RB — 79 rushing yards, 1 TD, 18 receiving yards" },
                { "josh jacobs", "RB — 84 rushing yards, 1 TD, 14 receiving yards" },
                { "breece hall", "RB — 91 rushing yards, 0 TD, 22 receiving yards" },
                { "isiah pacheco", "RB — 76 rushing yards, 1 TD, 11 receiving yards" },
                { "joe mixon", "RB — 73 rushing yards, 1 TD, 19 receiving yards" },
                { "alvin kamara", "RB — 62 rushing yards, 1 TD, 41 receiving yards" },
                { "aaron jones", "RB — 68 rushing yards, 0 TD, 27 receiving yards" },
                { "travis etienne", "RB — 82 rushing yards, 1 TD, 13 receiving yards" },
                { "kenneth walker", "RB — 74 rushing yards, 1 TD, 9 receiving yards" },
                { "raheem mostert", "RB — 66 rushing yards, 1 TD, 12 receiving yards" },
                { "dandre swift", "RB — 64 rushing yards, 0 TD, 18 receiving yards" },
                { "d'andre swift", "RB — 64 rushing yards, 0 TD, 18 receiving yards" },
                { "rhamondre stevenson", "RB — 71 rushing yards, 1 TD, 16 receiving yards" },

                // =========================
                // Wide Receivers
                // =========================
                { "tyreek hill", "WR — 155 receiving yards, 1 TD" },
                { "justin jefferson", "WR — 143 receiving yards, 2 TD" },
                { "ja'marr chase", "WR — 122 receiving yards, 1 TD" },
                { "jamar chase", "WR — 122 receiving yards, 1 TD" },
                { "ceedee lamb", "WR — 137 receiving yards, 1 TD" },
                { "ceede lamb", "WR — 137 receiving yards, 1 TD" }, // alias
                { "aj brown", "WR — 129 receiving yards, 1 TD" },
                { "a.j. brown", "WR — 129 receiving yards, 1 TD" },
                { "stefon diggs", "WR — 118 receiving yards, 1 TD" },
                { "davante adams", "WR — 112 receiving yards, 1 TD" },
                { "amon-ra st. brown", "WR — 121 receiving yards, 1 TD" },
                { "amon ra st brown", "WR — 121 receiving yards, 1 TD" },
                { "jaylen waddle", "WR — 104 receiving yards, 1 TD" },
                { "garrett wilson", "WR — 96 receiving yards, 1 TD" },
                { "tee higgins", "WR — 98 receiving yards, 1 TD" },
                { "devonta smith", "WR — 101 receiving yards, 1 TD" },
                { "puka nacua", "WR — 115 receiving yards, 1 TD" },
                { "cooper kupp", "WR — 108 receiving yards, 1 TD" },
                { "deebo samuel", "WR — 92 receiving yards, 1 TD, 24 rushing yards" },
                { "brandon aiyuk", "WR — 97 receiving yards, 1 TD" },
                { "mike evans", "WR — 106 receiving yards, 1 TD" },
                { "chris godwin", "WR — 88 receiving yards, 0 TD" },
                { "dk metcalf", "WR — 94 receiving yards, 1 TD" },
                { "keenan allen", "WR — 102 receiving yards, 1 TD" },
                { "terry mclaurin", "WR — 86 receiving yards, 0 TD" },
                { "amari cooper", "WR — 93 receiving yards, 1 TD" },

                // =========================
                // Tight Ends
                // =========================
                { "travis kelce", "TE — 89 receiving yards, 1 TD" },
                { "george kittle", "TE — 72 receiving yards, 1 TD" },
                { "mark andrews", "TE — 65 receiving yards, 1 TD" },
                { "tj hockenson", "TE — 78 receiving yards, 1 TD" },
                { "t.j. hockenson", "TE — 78 receiving yards, 1 TD" },
                { "sam laporta", "TE — 67 receiving yards, 1 TD" },
                { "dallas goedert", "TE — 61 receiving yards, 0 TD" },
                { "kyle pitts", "TE — 56 receiving yards, 0 TD" },
                { "darren waller", "TE — 58 receiving yards, 0 TD" },
                { "evan engram", "TE — 62 receiving yards, 0 TD" },
                { "david njoku", "TE — 54 receiving yards, 1 TD" },
                { "pat freiermuth", "TE — 49 receiving yards, 1 TD" },

                // =========================
                // Defensive Players
                // =========================
                { "micah parsons", "LB — 7 tackles, 2 sacks" },
                { "tj watt", "LB — 6 tackles, 1.5 sacks, 1 forced fumble" },
                { "t.j. watt", "LB — 6 tackles, 1.5 sacks, 1 forced fumble" },
                { "myles garrett", "DE — 5 tackles, 2 sacks, 1 FF" },
                { "nick bosa", "DE — 4 tackles, 1.5 sacks" },
                { "maxx crosby", "DE — 6 tackles, 1 sack" },
                { "fred warner", "LB — 9 tackles, 1 INT" },
                { "roquan smith", "LB — 11 tackles" },
                { "sauce gardner", "CB — 4 tackles, 2 pass breakups" },
                { "patrick surtain ii", "CB — 5 tackles, 1 PD" },
                { "derwin james", "S — 8 tackles, 1 INT" },
                { "jalen ramsey", "CB — 3 tackles, 1 INT" },
                { "trevon diggs", "CB — 4 tackles, 1 INT" },
                { "chris jones", "DT — 3 tackles, 1.5 sacks" },

                // =========================
                // Kickers
                // =========================
                { "justin tucker", "K — 3/3 field goals, long of 52 yards" },
                { "harrison butker", "K — 2/2 field goals, 4/4 extra points" },
                { "jake elliott", "K — 3/3 field goals, long of 54 yards" },
                { "evan mcpherson", "K — 2/2 field goals, 3/3 extra points" },
                { "brandon aubrey", "K — 3/3 field goals, 2/2 extra points" },
                { "jason myers", "K — 2/2 field goals, 3/3 extra points" },
                { "younghoe koo", "K — 2/2 field goals, 2/2 extra points" },
                { "daniel carlson", "K — 2/2 field goals, 2/2 extra points" }
            };

            // Try to find the closest matching player key in the dictionary for the user input.
            var matchKey = FindBestKey(stats.Keys, input);

            if (matchKey != null)
            {
                // Match found: show the user's original input and the stat summary for that key.
                lblStats.Text = $"{txtPlayer.Text}: {stats[matchKey]}";
            }
            else
            {
                // No suitable match: inform the user.
                lblStats.Text = "Player not found in football stats database.";
            }
        }

        // Attempts to find the best matching key among a set of names for the given input.
        // Matching strategy (in order):
        // 1) Exact match (case-insensitive)
        // 2) Normalized exact match (remove spaces/punctuation, compare lowercase alphanumerics)
        // 3) StartsWith
        // 4) Contains
        // 5) Levenshtein distance on normalized strings within a small threshold (1–3 depending on length)
        private static string FindBestKey(IEnumerable<string> keys, string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            // 1) Exact (case-insensitive handled by dictionary comparer when used) — simulate here on the key set
            var exact = keys.FirstOrDefault(k => string.Equals(k, input, StringComparison.OrdinalIgnoreCase));
            if (exact != null) return exact;

            // 2) Normalized exact (ignore spaces/punctuation) — helps with inputs like "aj brown" vs "a.j. brown"
            var normIn = Normalize(input);
            var normExact = keys.FirstOrDefault(k => Normalize(k) == normIn);
            if (normExact != null) return normExact;

            // 3) StartsWith (case-insensitive) — convenient for partial inputs like "patr" → "patrick mahomes"
            var starts = keys.FirstOrDefault(k => k.StartsWith(input, StringComparison.OrdinalIgnoreCase));
            if (starts != null) return starts;

            // 4) Contains (case-insensitive) — broader partial matching anywhere in the name
            var contains = keys.FirstOrDefault(k => k.IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0);
            if (contains != null) return contains;

            // 5) Levenshtein on normalized values with small threshold
            // This helps handle minor typos (e.g., "mahomes" vs "mahomes", "jevferson" vs "jefferson").
            string best = null;
            int bestDist = int.MaxValue;
            foreach (var k in keys)
            {
                var nk = Normalize(k);
                int dist = Levenshtein(normIn, nk);

                // Allow a small edit distance depending on the length of the strings
                int maxLen = Math.Max(normIn.Length, nk.Length);
                int threshold = maxLen <= 6 ? 1 : (maxLen <= 10 ? 2 : 3);

                if (dist < bestDist && dist <= threshold)
                {
                    bestDist = dist;
                    best = k;
                }
            }
            return best;
        }

        // Normalizes a string by:
        // - Keeping only letters and digits
        // - Lowercasing everything
        // This removes spaces and punctuation to make matching more forgiving.
        private static string Normalize(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            var arr = s.Where(char.IsLetterOrDigit).Select(char.ToLowerInvariant).ToArray();
            return new string(arr);
        }

        // Classic Levenshtein distance implementation.
        // Computes the minimum number of single-character edits (insertions, deletions, or substitutions)
        // required to change string a into string b.
        // Reference idea: https://en.wikipedia.org/wiki/Levenshtein_distance, https://www.geeksforgeeks.org/dsa/introduction-to-levenshtein-distance/
        private static int Levenshtein(string a, string b)
        {
            if (a == b) return 0;
            if (a.Length == 0) return b.Length;
            if (b.Length == 0) return a.Length;

            int[,] d = new int[a.Length + 1, b.Length + 1];

            // Initialize first row/column: converting from/to empty string
            for (int i = 0; i <= a.Length; i++) d[i, 0] = i;
            for (int j = 0; j <= b.Length; j++) d[0, j] = j;

            for (int i = 1; i <= a.Length; i++)
            {
                for (int j = 1; j <= b.Length; j++)
                {
                    int cost = (a[i - 1] == b[j - 1]) ? 0 : 1;

                    // Choose the cheapest edit: delete, insert, or substitute
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1,     // deletion
                                 d[i, j - 1] + 1),    // insertion
                        d[i - 1, j - 1] + cost        // substitution
                    );
                }
            }
            return d[a.Length, b.Length];
        }
    }
}