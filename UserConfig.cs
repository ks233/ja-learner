using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ja_learner
{
    internal class UserConfig
    {
        public static bool useExtraPrompt = false;
        private static string extraPromptFilename = "";

        public static string ExtraPromptFilename
        {
            get
            {
                return extraPromptFilename;
            }
            set
            {
                if (extraPromptFilename != value)
                {
                    extraPromptFilename = value;
                    UpdateExtraPrompt();
                }
            }
        }
        public static string ExtraPrompt { get; private set; } = string.Empty;

        public static void UpdateExtraPrompt()
        {
            var filePath = Path.Combine(Program.APP_SETTING.ExtraPromptDir, extraPromptFilename);
            try
            {
                ExtraPrompt = File.ReadAllText(filePath);
            }
            catch { }
        }


        public static string[] GetExtraPromptFiles()
        {
            return Directory.CreateDirectory(Program.APP_SETTING.ExtraPromptDir) // 如果文件夹不存在，创建文件夹
                    .GetFiles()
                    .Select(x => x.Name)
                    .ToArray(); 
        }
    }
}
