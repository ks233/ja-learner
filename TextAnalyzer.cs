using MeCab;
using OpenAI_API.Moderation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ja_learner
{

    struct TextAnalyzerResult
    {
        public string Surface;
        public string Pos;// 词性
        public string Basic;// 基本型
        public string Reading;// 渲染
        public readonly string ToJson()
        {

            return $"{{surface:'{Surface}',pos:'{Pos}',basic:'{Basic}',reading:'{Reading}'}}";
        }
    }


    // 包装 MecabDotNet
    internal class TextAnalyzer
    {
        private MeCabParam parameter;
        private MeCabTagger tagger;
        public TextAnalyzer() {
            // 初始化 mecab dotnet
            parameter = new MeCabParam();
            if (Directory.Exists("dic/userdic")){
                // 读取用户词典
                string[] userdic = Directory.GetFiles("dic/userdic");
                for (int i = 0; i < userdic.Length; i++)
                {
                    userdic[i] = "userdic/" + Path.GetFileName(userdic[i]);
                }
                parameter.UserDic = userdic;
            }
            tagger = MeCabTagger.Create(parameter);
        }

        public List<TextAnalyzerResult> Analyze(string text)
        {
            List<TextAnalyzerResult> results = new List<TextAnalyzerResult>();
            results.Clear();
            foreach (var node in tagger.ParseToNodes(text))
            {
                if (node.CharType > 0)
                {
                    TextAnalyzerResult result = new TextAnalyzerResult();
                    var features = node.Feature.Split(',');
                    // features[0] 是词性，[6] 是原型，[7] 是发音（如果有的话）
                    result.Surface = node.Surface;
                    result.Pos = features[0];
                    result.Basic = features[6];
                    result.Reading = features.Length > 7 ? features[7] : "";
                    results.Add(result);
                }
            }
            return results;
        }

        public string AnalyzeResultToJson(List<TextAnalyzerResult> results)
        {
            string result = "[";
            foreach (TextAnalyzerResult r in results)
            {
                result += $"{r.ToJson()},";
            }
            result += "]";
            return result;
        }
    }
}
