using MeCab;
using OpenAI_API.Moderation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ja_learner
{

    struct TextAnalyzerResult
    {
        public string Surface;
        public string Pos;// 词性
        public string Basic;// 基本型
        public string Reading;// 读音
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
                    // unidic里有些词的解析结果会有双引号，双引号里面有逗号，导致Split错位，所以先把双引号中间清空再Split
                    var features = Regex.Replace(node.Feature, "\"[^\"]*\"", "").Split(','); 

                    //MessageBox.Show(node.Feature);
                    // 这个mecab库好像没办法用dicrc自定义输出格式，dicrc只对个别属性生效
                    // 那只能用数组长度来判断了，如果很长说明是unidic，否则就是ipadic（默认词典）
                    result.Surface = node.Surface;
                    if (features.Length > 16) {
                        // unidic
                        result.Pos = features[0];
                        result.Basic = features[8];
                        result.Reading = features[20];
                        if (result.Reading == "*")
                        {
                            result.Reading = "";
                        }
                        if(Regex.IsMatch(result.Surface, @"^[a-zA-Z0-9]+$"))
                        {
                            result.Reading = "";
                        }
                    }
                    else if(features.Length > 6) 
                    {
                        // ipadic
                        result.Pos = features[0];
                        result.Basic = features[6];
                        result.Reading = features.Length > 7 ? features[7] : "";
                    }
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
