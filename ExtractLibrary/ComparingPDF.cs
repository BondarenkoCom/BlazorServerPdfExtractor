using ExtractLibrary.Helpers;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace ExtractLibrary
{
    [TestFixture]
    public class ComparingPDF
    {
        public Dictionary<string, (string, string, string)> TestCompareThree(string pathFirst, string pathSecond, string pathThird)
        {
            var differences12 = TestCompare(pathFirst, pathSecond);
            var differences13 = TestCompare(pathFirst, pathThird);

            var allKeys = new HashSet<string>(differences12.Keys.Union(differences13.Keys));

            var differences = new Dictionary<string, (string, string, string)>();
            foreach (var key in allKeys)
            {
                var val1 = differences12.ContainsKey(key) ? differences12[key].Item1 : null;
                var val2 = differences12.ContainsKey(key) ? differences12[key].Item2 : null;
                var val3 = differences13.ContainsKey(key) ? differences13[key].Item2 : null;

                differences[key] = (val1, val2, val3);
            }

            return differences;
        }

        //public Dictionary<string, (string, string)> TestCompare(string pathFirst, string pathSecond)
        public Dictionary<string, (string, string)> TestCompare(string pathFirst, string pathSecond)

        {
            var _readJsonFileHelper = new ReadJsonFileHelper();
            string jsonFilePath1 = pathFirst;
            string jsonFilePath2 = pathSecond;

            JObject jsonFile1 = _readJsonFileHelper.ReadJsonFile(jsonFilePath1);
            JObject jsonFile2 = _readJsonFileHelper.ReadJsonFile(jsonFilePath2);

            Dictionary<string, (string, string)> differences
                = new Dictionary<string, (string, string)>();
            CompareTokens(jsonFile1, jsonFile2, differences);

            return differences;
        }

        private void CompareTokens(JToken token1, JToken token2, Dictionary<string, (string, string)> differences, string path = "")
        {
            if (token1.Type != token2.Type)
            {
                differences[path] = (token1.ToString(), token2.ToString());
            }
            else if (token1.Type == JTokenType.Object)
            {
                var obj1 = (JObject)token1;
                var obj2 = (JObject)token2;

                foreach (var prop in obj1.Properties())
                {
                    // Ignore the specified properties
                    if (prop.Name.Equals("extended_metadata") || prop.Name.Equals("Bounds") || prop.Name.Equals("Extended Bounds"))
                        continue;

                    var propPath = !string.IsNullOrEmpty(path) ? $"{path}.{prop.Name}" : prop.Name;
                    if (obj2.ContainsKey(prop.Name))
                    {
                        CompareTokens(prop.Value, obj2[prop.Name], differences, propPath);
                    }
                    else
                    {
                        differences[propPath] = (prop.Value.ToString(), null);
                    }
                }

                foreach (var prop in obj2.Properties())
                {
                    // Ignore the specified properties
                    if (prop.Name.Equals("extended_metadata") || prop.Name.Equals("Bounds") 
                        || prop.Name.Equals("Extended Bounds") || prop.Name.Equals("ClipBounds"))
                        continue;

                    var propPath = !string.IsNullOrEmpty(path) ? $"{path}.{prop.Name}" : prop.Name;
                    if (!obj1.ContainsKey(prop.Name))
                    {
                        differences[propPath] = (null, prop.Value.ToString() + " (No such element in file 1)");
                    }
                }
            }

            else if (token1.Type == JTokenType.Array)
            {
                var arr1 = (JArray)token1;
                var arr2 = (JArray)token2;

                int maxLength = Math.Max(arr1.Count, arr2.Count);

                for (int i = 0; i < maxLength; i++)
                {
                    if (i < arr1.Count && i < arr2.Count)
                    {
                        CompareTokens(arr1[i], arr2[i], differences, $"{path}[{i}]");
                    }
                    else if (i < arr1.Count)
                    {
                        differences[$"{path}[{i}]"] = (arr1[i].ToString(), null);
                    }
                    else
                    {
                        differences[$"{path}[{i}]"] = (null, arr2[i].ToString());
                    }
                }
            }
            else if (!JToken.DeepEquals(token1, token2))
            {
                differences[path] = (token1.ToString(), token2.ToString());
            }
            else
            {
                differences[path] = (token1.ToString(), "No differences with file 1 or this element does not exist.");
            }
        }
    }
}