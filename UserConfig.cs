using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ja_learner
{
    internal class UserConfig
    {
        public static string api_key = "";
        public static string api_url = "";

        public static void ReadConfigFile()
        {
            string filePath = "config.txt";

            // 逐行读取config.txt，第一行key第二行url
            using (StreamReader reader = new StreamReader(filePath))
            {
                try
                {
                    api_key = reader.ReadLine();
                    api_url = reader.ReadLine();
                }
                catch { }
            }
        }
    }
}
