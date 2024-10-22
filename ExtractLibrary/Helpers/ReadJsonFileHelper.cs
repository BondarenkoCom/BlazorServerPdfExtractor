﻿using Newtonsoft.Json.Linq;

namespace ExtractLibrary.Helpers
{
    public class ReadJsonFileHelper
    {
        public JObject ReadJsonFile(string filePath)
        {
            using StreamReader reader = new StreamReader(filePath);
            string json = reader.ReadToEnd();
            JObject jObject = JObject.Parse(json);
            return jObject;
        }
    }
}
