# ja-learner

![title](README/title.png)

## 介绍

这是一款简单实用的日语学习 / 视觉小说阅读辅助工具，提供了句子拆解、汉字注音、外来语标注、快速查词、参考翻译、ChatGPT 分析等功能，降低日文阅读障碍。

### 特性

- 使用不同颜色显示不同的句子成分，在汉字上方标注读音，在外来语上方标注英语，使句子更加清晰易读。
- 点击单词快速查询 MOJi 辞書，哪里不会点哪里。
- 支持谷歌与 ChatGPT 参考翻译。
- 调用 ChatGPT 解说句子中的单词和语法成分。
- 吸附窗口、读取剪贴板实时更新文本，配合游戏文本提取工具，把视觉小说变成日语课本。

## 使用说明

### 基本的句子分析  

- 读取剪贴板或手动输入句子。
- 勾选“片假不留”可以把片假名单词翻译成英文。
  - 点击片假名单词上方的英文可以隐藏该单词的翻译，再点一下恢复显示，用于屏蔽错误的翻译结果。
- 可以用 Ctrl + 滚轮调整分析界面的显示大小。
  - 本质浏览器套壳，你甚至可以按 F12 打开控制台（
- 点击单词会弹出词典窗口，显示 MOJi 辞書的搜索结果。

### 窗口吸附

在主窗口的“系统设置”面板中，首先点击“选择窗口”按钮，然后把鼠标移到想要吸附的窗口，点一下左键。此时右边选框中的文字会变成“与 xxx 对齐”，把选框勾上，窗口就吸附到游戏窗口边上了。

![attach](README/attach.gif)

### 游戏文本提取

本项目**并没有**内置游戏文本提取的功能，但是可以实时读取剪贴板。建议使用 [Textractor](https://github.com/Artikash/Textractor)、[manga-ocr](https://github.com/kha-white/manga-ocr) 等文本提取工具将游戏文本提取至剪贴板，然后在本软件中勾选“读取剪贴板”，即可实时同步游戏文本。配合 Textractor 的使用效果如图：

![text-extraction](README/text-extraction.gif)

### 参考翻译

目前支持了谷歌翻译和 GPT 翻译。其中谷歌翻译无需配置，可以免费无限制使用，而 GPT 需要配置 API Key，消耗 API 余额。

#### 谷歌翻译 & 谷歌生草机

这是两个不同的接口，“谷歌翻译”会得到与网页版谷歌翻译相同的翻译结果，“谷歌生草机”的翻译结果与网页版不同，质量普遍低于网页版。

#### 使用 GPT（需要 API Key）

首先要配置 api key，在 `config.txt` 的第一行输入 api key，第二行输入 api url：

```
sk-xxxxxxxxx
https://api.openai.com/{0}/{1}
```

如果你使用第三方反代，就要将第二行修改为相应的域名。

配置好 api key 就可以使用 GPT 翻译和解说文本了。

![gpt](README/gpt.gif)

## 声明

### 分析与翻译仅供参考

本项目的分词与注音功能基于形态分析器 MeCab，MeCab 有时会犯一些低级错误，比如把「身体（からだ）」注音为「しんたい」、把「二人（ふたり）」注音为「ににん」，用词汇更丰富的 [UniDic](https://clrd.ninjal.ac.jp/unidic/) 词典替换 `dic` 文件夹中默认的 IPADIC 效果会稍好一些。

至于翻译，机翻懂的都懂。谷歌翻译遇到复杂的句式和不规范的表达就容易翻车，ChatGPT 比谷歌懂更多俗语、流行语，但偶尔也会发癫，比如使用简体中文以外的语言回复、唐突的[塞氏翻译法](https://zh.moegirl.org.cn/zh-hans/塞氏翻译法)等等。建议把本软件当做一个精读工具而不是翻译器，把注意力放在日语原文上，只在不确定的时候使用翻译作为参考。

外来语标注功能使用谷歌翻译将片假名单词翻译为英语，但不是所有片假名单词都是外来语，外来语也不一定来源于英语，还有像 supplies 和 surprise 这样的“同音词”也不好区分，因此也会出现标记错误的情况。

根据我个人的使用体验，整体准确率还可以接受，但还是不建议完全初学者使用，以免被误导。如果遇到可疑的注音或翻译，建议点击单词查看 MOJi 辞書的解释和注音，并对照不同引擎的翻译结果，或者使用 ChatGPT 的解说功能。

### 相关项目与第三方库

- 开坑的想法主要来源于 [YUKI 翻译器](https://github.com/project-yuki/YUKI) 和 [Translation-Aggregator](https://github.com/Translation-Aggregator/Translation-Aggregator)，前者支持了丰富的翻译接口，内置了文本提取功能，但使用起来比较复杂，且缺少快速查词的功能；后者虽然可以鼠标悬停查词，但只有日英词典、界面比较古老，而且翻译接口几乎炸完了，于是我决定搓一个更简单、更符合自己需求的工具。
- 在开发过程中，[taishi-i/awesome-japanese-nlp-resources](https://github.com/taishi-i/awesome-japanese-nlp-resources) 中丰富的日语相关资料对我帮助很大。
- 相关功能实现：
  - 形态分析与注音： [kekyo/MeCab.DotNet](https://github.com/kekyo/MeCab.DotNet)
  - 调用 ChatGPT：[OkGoDoIt/OpenAI-API-dotnet](https://github.com/OkGoDoIt/OpenAI-API-dotnet)
  - 谷歌翻译：参考了 [FilipePS/Traduzir-paginas-web](https://github.com/FilipePS/Traduzir-paginas-web) 的 API 调用方式。
  - 单词搜索：[MOJi 辞書](https://www.mojidict.com/)
  - Webview 页面：Vite + Vue，这部分在另一个项目中，目前暂未开源。

## 下载 & bug 反馈

在 [Releases](https://github.com/ks233/ja-learner/releases) 可以下载到软件的最新版本，另外欢迎在 [Issues](https://github.com/ks233/ja-learner/issues) 反馈 bug，如果觉得好用可以考虑给个 star，谢啦。
