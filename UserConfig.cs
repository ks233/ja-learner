using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ja_learner
{
    internal class UserConfig
    {
        public static string apiKey = "";
        public static string apiUrl = "";
        public static string extraPrompt = "";
        public static bool useExtraPrompt = false;

        public static void ReadConfigFile()
        {
            string filePath = "config.txt";
            // 逐行读取config.txt，第一行key第二行url
            using (StreamReader reader = new StreamReader(filePath))
            {
                try
                {
                    apiKey = reader.ReadLine();
                    apiUrl = reader.ReadLine();
                }
                catch { }
            }
            UpdateExtraPrompt();
        }

        public static void UpdateExtraPrompt()
        {
            string filePath = "extra_prompt.txt";
            try
            {
                extraPrompt = File.ReadAllText(filePath);
            }
            catch { }
        }
    }
}
