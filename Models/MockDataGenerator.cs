using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace MyShop.Models
{
    public class MockDataGenerator<T>
    {
        
        public static List<T> GetMockItems(string JsonFileName) => GetItemsFromJSON(JsonFileName);
        private static List<T> GetItemsFromJSON(string jsonFileName)
        {
            //var fullpath = Environment.CurrentDirectory.ToString() + $"/MockData/{jsonFileName}";
            //var jsonString = File.ReadAllText(fullpath);
            //var jsonData = JsonSerializer.Deserialize<T>(jsonString);
            using (StreamReader r = new StreamReader(Environment.CurrentDirectory.ToString() + $"/MockData/{jsonFileName}"))
            {
                string json = r.ReadToEnd();
                if (string.IsNullOrWhiteSpace(json)) return null;
                var Items = JsonConvert.DeserializeObject<List<T>>(json);
                if (Items == null || Items.Count == 0) return null;
                return Items;
            }
        }
       

    }
}
