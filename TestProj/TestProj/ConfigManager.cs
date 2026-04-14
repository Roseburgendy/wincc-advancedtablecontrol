using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestProj
{
    internal class ConfigManager
    {
        public class TagMeta
        {
            public string tagName { get; set; }
            public string displayName { get; set; }
            public double normalMin { get; set; }
            public double normalMax { get; set; }
            public int decimalPlaces { get; set; } = 2; // 2 by default
            public int order { get; set; }
        }

        // 供外部调用：传入 JSON 字符串，返回列表
        public static List<TagMeta> ParseFromJson(string json)
        {
            return JsonConvert.DeserializeObject<List<TagMeta>>(json);
        }
        static void main(string[] args)
        {
            string json = @"[
            {
                'tagName': 'tag1',
                'displayName': 'Tag 1',
                'normalMin': 0,
                'normalMax': 100,
                'decimalPlaces': 2,
                'order': 1
            },
            {
                'tagName': 'tag2',
                'displayName': 'Tag 2',
                'normalMin': -50,
                'normalMax': 50,
                'decimalPlaces': 1,
                'order': 2
            }
        ]";
            List<TagMeta> deserialized = JsonConvert.DeserializeObject<List<TagMeta>>(json);
            foreach (var tag in deserialized)
            {
                Console.WriteLine(tag.tagName);
                Console.WriteLine(tag.displayName);
                Console.WriteLine(tag.normalMin);
                Console.WriteLine(tag.normalMax);
                Console.WriteLine(tag.decimalPlaces);
                Console.WriteLine(tag.order);
            }

            Console.ReadKey();
        }
    }
}
