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

            // 使用StreamReader打开文件以逐行读取
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
