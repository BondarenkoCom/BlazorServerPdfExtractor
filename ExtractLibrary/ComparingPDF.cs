using ExtractLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.IO;

namespace ExtractLibrary
{
    [TestFixture]
    public class ComparingPDF
    {
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
                    var propPath = !string.IsNullOrEmpty(path) ? $"{path}.{prop.Name}" : prop.Name;
                    if (!obj1.ContainsKey(prop.Name))
                    {
                        differences[propPath] = (null, prop.Value.ToString());
                    }
                }
            }

            else if (token1.Type == JTokenType.Array)
            {
                var arr1 = (JArray)token1;
                var arr2 = (JArray)token2;

                for (int i = 0; i < arr1.Count; i++)
                {
                    if (i < arr2.Count)
                    {
                        CompareTokens(arr1[i], arr2[i], differences, $"{path}[{i}]");
                    }
                    else
                    {
                        differences[$"{path}[{i}]"] = (arr1[i].ToString(), null);
                    }
                }

                for (int i = arr1.Count; i < arr2.Count; i++)
                {
                    differences[$"{path}[{i}]"] = (null, arr2[i].ToString());
                }
                
            }
            else if (!JToken.DeepEquals(token1, token2))
            {
                differences[path] = (token1.ToString(), token2.ToString());
            }
        }
    }
}