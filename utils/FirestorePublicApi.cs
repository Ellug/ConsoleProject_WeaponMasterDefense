using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WeaponMasterDefense
{
    public static class FirestorePublicApi
    {
        private const string ProjectId = "wmdefenceranking";
        private static readonly string BaseUrl = $"https://firestore.googleapis.com/v1/projects/{ProjectId}/databases/(default)";

        private static readonly HttpClient Http = new HttpClient();

        // TOP10 조회
        public static async Task<List<(string Name, int Score, string DocName)>> GetTop10Async()
        {
            var url = $"{BaseUrl}/documents:runQuery";
            var body = new
            {
                structuredQuery = new
                {
                    from = new[] { new { collectionId = "Ranking" } },
                    orderBy = new[] { new { field = new { fieldPath = "Score" }, direction = "DESCENDING" } },
                    limit = 10
                }
            };

            var resp = await Http.PostAsync(url, JsonContent(body));
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            var arr = JArray.Parse(json);

            var list = new List<(string, int, string)>();
            foreach (var item in arr)
            {
                var doc = item["document"];
                if (doc == null) continue;
                string docName = doc.Value<string>("name");

                var fields = doc["fields"] as JObject;
                if (fields == null) continue;

                string name = fields["Name"]?["stringValue"]?.ToString() ?? "UNKNOWN";

                int score = 0;
                var scoreToken = fields["Score"];
                if (scoreToken?["integerValue"] != null)
                    int.TryParse(scoreToken["integerValue"]?.ToString(), out score);

                list.Add((name, score, docName));
            }
            return list;
        }

        // 점수 추가
        public static async Task AddToDoc(string name, int score)
        {
            var url = $"{BaseUrl}/documents/Ranking";
            var body = new
            {
                fields = new
                {
                    Name = new { stringValue = name },
                    Score = new { integerValue = score }
                }
            };
            var resp = await Http.PostAsync(url, JsonContent(body));
            resp.EnsureSuccessStatusCode();
        }

        // TOP10만 남기고 나머지 삭제
        public static async Task DeleteOutRank()
        {
            // 점수 내림차순으로 전체 가져온 다음 11등 이후 삭제
            var url = $"{BaseUrl}/documents:runQuery";
            var body = new
            {
                structuredQuery = new
                {
                    from = new[] { new { collectionId = "Ranking" } },
                    orderBy = new[] { new { field = new { fieldPath = "Score" }, direction = "DESCENDING" } },
                }
            };

            var resp = await Http.PostAsync(url, JsonContent(body));
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            var arr = JArray.Parse(json);

            var docNames = new List<string>();
            foreach (var item in arr)
            {
                var doc = item["document"];
                if (doc == null) continue;
                string docName = doc.Value<string>("name");
                if (!string.IsNullOrEmpty(docName)) docNames.Add(docName);
            }

            for (int i = 10; i < docNames.Count; i++)
            {
                var del = await Http.DeleteAsync($"{BaseUrl}/documents/{DocumentPath(docNames[i])}");
                del.EnsureSuccessStatusCode();
            }
        }

        private static StringContent JsonContent(object o) => new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");

        private static string DocumentPath(string fullName)
        {
            var idx = fullName.IndexOf("/documents/", StringComparison.OrdinalIgnoreCase);
            return idx >= 0 ? fullName.Substring(idx + "/documents/".Length) : fullName;
        }
    }
}
