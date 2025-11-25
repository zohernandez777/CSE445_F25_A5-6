using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

//Note this is the remnote service implmermntationation
namespace Assignment5_CSE445F25
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TeamStatsSeervice" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TeamStatsSeervice.svc or TeamStatsSeervice.svc.cs at the Solution Explorer and start debugging.
    public class TeamStatsService : ITeamStatsService
    {
        public string GetTeamStats(string teamName)
        {
            // Reject empty/whitespace inputs early.
            if (string.IsNullOrWhiteSpace(teamName))
                return "Team name is required.";

            // Normalize the input for better matching (e.g., "the eagles" -> "eagles").
            string input = teamName.Trim();
            if (input.StartsWith("the ", StringComparison.OrdinalIgnoreCase))
                input = input.Substring(4).Trim();

            // Build a case-insensitive dictionary of team aliases -> summary text.
            var stats = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            // Local function to register one summary under many aliases (full name, city, abbr, nicknames).
            void AddTeam(string summary, params string[] aliases)
            {
                foreach (var a in aliases)
                {
                    var k = a.Trim();           // normalize the alias key
                    if (!stats.ContainsKey(k))  // avoid duplicates/overwrites
                        stats[k] = summary;     // map alias to the team summary
                }
            }

            // 32 teams with common aliases and abbreviations (sample summaries)
            //NOTE: Once again these stats are made up for the purpose of this assignment, later in assignment 6 ill make sure to pull from a SQL database or API
            AddTeam("Arizona Cardinals — 24 points, 310 total yards, 2 turnovers.",
                "cardinals", "arizona", "arizona cardinals", "ari");

            AddTeam("Atlanta Falcons — 23 points, 342 total yards, 1 turnover.",
                "falcons", "atlanta", "atlanta falcons", "atl");

            AddTeam("Baltimore Ravens — 30 points, 397 total yards, 0 turnovers.",
                "ravens", "baltimore", "baltimore ravens", "bal");

            AddTeam("Buffalo Bills — 28 points, 389 total yards, 1 turnover.",
                "bills", "buffalo", "buffalo bills", "buf");

            AddTeam("Carolina Panthers — 17 points, 298 total yards, 2 turnovers.",
                "panthers", "carolina", "carolina panthers", "car");

            AddTeam("Chicago Bears — 20 points, 321 total yards, 1 turnover.",
                "bears", "chicago", "chicago bears", "chi");

            AddTeam("Cincinnati Bengals — 27 points, 374 total yards, 1 turnover.",
                "bengals", "cincinnati", "cincinnati bengals", "cin");

            AddTeam("Cleveland Browns — 22 points, 335 total yards, 2 turnovers.",
                "browns", "cleveland", "cleveland browns", "cle");

            AddTeam("Dallas Cowboys — 29 points, 385 total yards, 1 turnover.",
                "cowboys", "dallas", "dallas cowboys", "dal");

            AddTeam("Denver Broncos — 21 points, 318 total yards, 1 turnover.",
                "broncos", "denver", "denver broncos", "den");

            AddTeam("Detroit Lions — 26 points, 372 total yards, 1 turnover.",
                "lions", "detroit", "detroit lions", "det");

            AddTeam("Green Bay Packers — 24 points, 346 total yards, 1 turnover.",
                "packers", "green bay", "green bay packers", "gb", "gnb");

            AddTeam("Houston Texans — 27 points, 361 total yards, 0 turnovers.",
                "texans", "houston", "houston texans", "hou");

            AddTeam("Indianapolis Colts — 23 points, 333 total yards, 1 turnover.",
                "colts", "indianapolis", "indianapolis colts", "ind");

            AddTeam("Jacksonville Jaguars — 25 points, 358 total yards, 1 turnover.",
                "jaguars", "jacksonville", "jacksonville jaguars", "jax", "jags");

            AddTeam("Kansas City Chiefs — 27 points, 402 total yards, 0 turnovers.",
                "chiefs", "kansas city", "kansas city chiefs", "kc", "kan");

            AddTeam("Las Vegas Raiders — 22 points, 341 total yards, 1 turnover.",
                "raiders", "las vegas", "las vegas raiders", "lv", "lvr", "oakland raiders", "oak");

            AddTeam("Los Angeles Chargers — 24 points, 368 total yards, 1 turnover.",
                "chargers", "los angeles chargers", "la chargers", "lac", "san diego chargers", "sd");

            AddTeam("Los Angeles Rams — 23 points, 352 total yards, 1 turnover.",
                "rams", "los angeles rams", "la rams", "lar", "st louis rams", "stl");

            AddTeam("Miami Dolphins — 33 points, 450 total yards, 2 turnovers.",
                "dolphins", "miami", "miami dolphins", "mia");

            AddTeam("Minnesota Vikings — 24 points, 349 total yards, 1 turnover.",
                "vikings", "minnesota", "minnesota vikings", "min");

            AddTeam("New England Patriots — 19 points, 312 total yards, 1 turnover.",
                "patriots", "new england", "new england patriots", "ne", "nwe");

            AddTeam("New Orleans Saints — 22 points, 329 total yards, 1 turnover.",
                "saints", "new orleans", "new orleans saints", "no", "nor");

            AddTeam("New York Giants — 20 points, 318 total yards, 1 turnover.",
                "giants", "new york giants", "nyg", "n.y. giants");

            AddTeam("New York Jets — 21 points, 305 total yards, 1 turnover.",
                "jets", "new york jets", "nyj", "n.y. jets");

            AddTeam("Philadelphia Eagles — 31 points, 412 total yards, 1 turnover.",
                "eagles", "philadelphia", "philadelphia eagles", "phi");

            AddTeam("Pittsburgh Steelers — 20 points, 301 total yards, 0 turnovers.",
                "steelers", "pittsburgh", "pittsburgh steelers", "pit");

            AddTeam("San Francisco 49ers — 34 points, 428 total yards, 0 turnovers.",
                "49ers", "niners", "san francisco", "san francisco 49ers", "sf", "sfo");

            AddTeam("Seattle Seahawks — 24 points, 343 total yards, 1 turnover.",
                "seahawks", "seattle", "seattle seahawks", "sea");

            AddTeam("Tampa Bay Buccaneers — 25 points, 337 total yards, 0 turnovers.",
                "buccaneers", "bucs", "tampa bay", "tampa bay buccaneers", "tb", "tam");

            AddTeam("Tennessee Titans — 21 points, 314 total yards, 1 turnover.",
                "titans", "tennessee", "tennessee titans", "ten");

            AddTeam("Washington Commanders — 22 points, 327 total yards, 2 turnovers.",
                "commanders", "washington", "washington commanders", "was", "wsh", "washington football team", "wft");

            // Fuzzy match across all registered keys/aliases to pick the best candidate.
            var matchKey = FindBestKey(stats.Keys, input);
            if (matchKey != null)
                return stats[matchKey];

            return "Team not found in stats database.";
        }

        /// <summary>
        /// Attempts to find the best matching key among a set of names for the given input.
        /// Strategy (in order):
        /// 1) Exact (case-insensitive)
        /// 2) Normalized exact (remove spaces/punctuation, compare lowercase alphanumerics)
        /// 3) StartsWith (case-insensitive)
        /// 4) Contains (case-insensitive)
        /// 5) Levenshtein distance on normalized strings within a small threshold
        /// </summary>
        private static string FindBestKey(IEnumerable<string> keys, string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            // 1) Exact (case-insensitive)
            var exact = keys.FirstOrDefault(k => string.Equals(k, input, StringComparison.OrdinalIgnoreCase));
            if (exact != null) return exact;

            // 2) Normalized exact (ignore spaces/punctuation)
            var normIn = Normalize(input);
            var normExact = keys.FirstOrDefault(k => Normalize(k) == normIn);
            if (normExact != null) return normExact;

            // 3) StartsWith (case-insensitive)
            var starts = keys.FirstOrDefault(k => k.StartsWith(input, StringComparison.OrdinalIgnoreCase));
            if (starts != null) return starts;

            // 4) Contains (case-insensitive)
            var contains = keys.FirstOrDefault(k => k.IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0);
            if (contains != null) return contains;

            // 5) Levenshtein on normalized values with a small threshold:
            //    handle minor typos while avoiding spurious matches on very different strings.
            string best = null;
            int bestDist = int.MaxValue;
            foreach (var k in keys)
            {
                var nk = Normalize(k);
                int dist = Levenshtein(normIn, nk);

                // Allow a small edit distance based on length:
                // - <= 6 chars: 1 edit
                // - <= 10 chars: 2 edits
                // - > 10 chars: 3 edits
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

        /// <summary>
        /// Normalizes a string by keeping only letters/digits and lowercasing.
        /// This removes spaces and punctuation to make matching more forgiving
        /// (e.g., "N.Y. Giants" -> "nygiants").
        /// </summary>
        private static string Normalize(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            var arr = s.Where(char.IsLetterOrDigit).Select(char.ToLowerInvariant).ToArray();
            return new string(arr);
        }

        /// <summary>
        /// Classic Levenshtein distance (dynamic programming).
        /// Returns the minimum number of edits (insert, delete, substitute) to transform a into b.
        /// </summary>
        private static int Levenshtein(string a, string b)
        {
            if (a == b) return 0;
            if (a.Length == 0) return b.Length;
            if (b.Length == 0) return a.Length;

            int[,] d = new int[a.Length + 1, b.Length + 1];

            // Initialize first row/column: converting from/to empty string.
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