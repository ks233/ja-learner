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
        private const string EXTRA_PROMPT_DIR = "extra_prompts";
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
        public static string extraPrompt = "";

        public static string ExtraPrompt
        {
            get
            {
                return extraPrompt;
            }
        }


        public static void ReadConfigFile()
        {
            UpdateExtraPrompt();
        }

        public static void UpdateExtraPrompt()
        {
            string filePath = $"{EXTRA_PROMPT_DIR}/{extraPromptFilename}";
            try
            {
                extraPrompt = File.ReadAllText(filePath);
            }
            catch { }
        }


        public static string[] GetExtraPromptFiles()
        {
            string path = EXTRA_PROMPT_DIR;
            Directory.CreateDirectory(path); // 如果文件夹不存在，创建文件夹
            string[] files = Directory.GetFiles(path);
            for (int i = 0; i < files.Length; i++)
            {
                files[i] = Path.GetFileName(files[i]);
            }
            return files;
        }
    }
}
