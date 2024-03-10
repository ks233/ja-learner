<h1 align="center">KS的日语学习工具 v0.4</h1>
<div align="center">
    <strong>📖 简易日语学习 / 视觉小说阅读辅助工具</strong>
    <br />
    <span>句子拆解 • 汉字注音 • 一键查词 • 参考翻译 • 外来语标注 • AI讲解</span>
</div> 
<h3 align="center">
    <a href="https://github.com/ks233/ja-learner/releases">下载</a><span> • </span><a href="https://github.com/ks233/ja-learner/issues">Bug 反馈</a>
</h3>
<div align="center">
    <a href="https://github.com/ks233/ja-learner">
        <img src="README/title.png" alt="Title" >
    </a>
</div>




## 功能介绍

* **语句分析**：用不同样式区分句子成分，为句子中的汉字注音
* **单词查询**：点击单词一键查询 MOJi 辞書，哪里不会点哪里
* **参考翻译**：支持谷歌翻译与 ChatGPT 翻译，把握句子整体含义
* **片假不留**：在片假名上方显示英语翻译，满屏片假名也不怕
* **游戏文本分析**：吸附并跟随游戏窗口，配合文本提取工具，实时分析游戏文本
* **AI 讲解**：调用 ChatGPT 讲解句子中的单词和语法成分
* **添加 Anki 卡片**：快速添加单词卡片，打造自己的单词本


## 使用说明

### 分析与查词

* 读取剪贴板或手动输入句子
* 勾选“片假不留”把片假名单词翻译成英文
  * 点击片假名单词上方的英文可以隐藏该单词的翻译，再点一下恢复显示，用于屏蔽错误的翻译结果
* 可以用 Ctrl + 滚轮调整分析界面的显示大小
  * 本质浏览器套壳，你甚至可以按 F12 打开控制台（
* 在语句分析界面点击单词快速查词
  * 左键点击单词：在词典窗口中显示 MOJi 辞書的搜索结果（原型）
  * 中键点击单词：在浏览器中打开单词在 MOJi 辞書的搜索页面（原型）
  * SHIFT + 左键点击单词：在搜索内容末尾追加单词（表层形）
* 词典窗口手动搜索
  * 双击搜索框清空搜索内容
  * 点击链接跳转至对应的词典网站

![demo](README/demo.gif)

搜索框与 SHIFT 追加搜索是 v0.3 新增的功能，MeCab 有时候会过度断句，比如把“二十四节气”当成了三个词，“四”的注音还标错了。用追加搜索功能就可以把拆开的词拼回来，使查词更加灵活。


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

> 如果你的网络环境无法访问谷歌翻译，可以尝试使用 [GoodCoder666/GoogleTranslate_IPFinder](https://github.com/GoodCoder666/GoogleTranslate_IPFinder) 等工具扫描可用 IP，然后修改 HOST。

#### 使用 GPT（需要 API Key）

在 `appsettings.json` 中配置 GPT 相关字段：

```json
"GPT": {
    "ApiKey": "sk-xxx",
    "ApiUrl": "https://api.openai.com/{0}/{1}", // 实际调用为https://api.openai.com/v1/chat/completions
    "ExtraPromptDir": "extra_prompts",			// 额外的Prompt，比如指定某些角色名字怎么翻译
    "TranslatePrompt": "...",					// 翻译Prompt
    "ExplainPrompt": "..."						// 分析Prompt
}
```

一般只需要配置 ApiKey 就行，如果使用非官方 API 请按照格式修改 ApiUrl，如果对默认的 Prompt 不满意也可以自行更改。

配置好 ApiKey 就可以使用 ChatGPT 翻译和解说文本了。

![gpt](README/gpt.gif)

### 沉浸模式

- 双击分析页面的背景，进入仅显示语句分析的沉浸模式
- CTRL + 左键拖动窗口
- 左键拖动窗口边缘调整窗口大小

![immersive](README/immersive.gif)

### 添加 Anki 卡片

Anki 是一款经典的记忆卡片软件，它的设计理念影响了很多背单词软件。如果你不了解 Anki，可以看这个[简短的介绍视频](https://www.bilibili.com/video/BV1hz4y1U7H3/)。

在 `appsettings.json` 中，与 Anki 有关的字段有以下这些：

```json
"AnkiEnabled": true,
"Anki": {
    "AnkiConnectUrl": "http://127.0.0.1:8765", // AnkiConnect 默认端口
    "Deck": "test",				// 添加卡片的目标牌组
    "Model": "ja-learner",		// 卡片的模板名
    "FieldNames":{				// 模板中的字段
        "Word": "单词",			// 存储单词的字段名
        "Example": "例句",		// 存储例句的字段名
        "Explain": "解释"			// 存储解释的字段名
    }
}
```

使用 Anki 的准备工作如下：

- `AnkiEnabled` 默认为 false，改为 true 以显示与 Anki 有关的界面。
- 安装 Anki，为了使软件能连接 Anki，安装 [AnkiConnect](https://ankiweb.net/shared/info/2055492159) 插件，插件默认端口是 8765。
- 准备一个牌组，新建牌组或者使用现有的牌组都行。
- 在 Anki 中创建新的卡牌模板
  - 用三个字段分别存放单词、例句、解释。
  - 编辑卡牌模板的显示方式，比如把单词和例句显示在正面，解释显示在背面
- 将 `Deck` 以及下面的几项修改为对应的牌组名称、模板名称、字段名称。
  - 注意核对牌组与字段名称，如果牌组/模板/字段不存在，或者名称不一致，将无法添加卡片。


完成配置后只要在词典页面点击“添加到Anki”，就可以一键将当前文本例句、MOJi 单词和解释创建为新卡片，添加到卡组中。

自动导入的单词卡片如图所示：

![anki](README/anki.png)

## 使用建议

本项目的分词与注音功能基于 MeCab，虽然整体准确率还算可以，但有时会犯一些低级错误，比如在某些语境下把<ruby>身体<rt>からだ</rt></ruby>注音为 しんたい、把<ruby>二人<rt>ふたり</rt></ruby>注音为 ににん，遇到读音特殊的人名也无法正确注音。用词汇更丰富的 [UniDic](https://clrd.ninjal.ac.jp/unidic/) 词典替换 `dic` 文件夹中默认的 IPADIC 效果会稍好一些。

翻译毕竟都是机翻，准确率有限。谷歌翻译日译中会用英语作为中间语言，有时候结果很怪；ChatGPT 比谷歌懂更多俗语、流行语，但比较不稳定，偶尔会使用简体中文以外的语言回复、唐突地使用[塞氏翻译法](https://zh.moegirl.org.cn/zh-hans/塞氏翻译法)。建议把本软件当做一个精读工具而不是翻译器，把注意力放在日语原文上，只在不确定的时候使用翻译作为参考。

外来语标注功能使用谷歌翻译将片假名单词翻译为英语，但不是所有片假名单词都是外来语，外来语也不一定来源于英语，还有像 supplies 和 surprise 这样的“同音词”也不好区分，因此也会出现标注错误的情况。

根据作者自己的使用体验，整体准确率还可以接受，但还是不建议完全初学者使用，以免被误导。如果遇到可疑的注音或翻译，建议查询更权威的词典，比如 [Weblio 辞書](https://www.weblio.jp/)、大辞林、小学馆日中，网络用语可以查 [ニコニコ大百科](https://dic.nicovideo.jp/)。

## 相关项目

开坑的想法主要来源于 [YUKI 翻译器](https://github.com/project-yuki/YUKI) 和 [Translation-Aggregator](https://github.com/Translation-Aggregator/Translation-Aggregator)，前者支持了丰富的翻译接口，内置了文本提取功能，但使用起来比较复杂，且缺少快速查词的功能；后者虽然可以鼠标悬停查词，但只有日英词典、界面比较古老，而且翻译接口几乎炸完了，于是我决定搓一个更简单、更符合自己需求的工具。

v0.4 更新了添加 Anki 卡片的功能，想法来源于 [2DIPW/novel2anki](https://github.com/2DIPW/novel2anki) 和 [Yomichan](https://foosoft.net/projects/yomichan/)。novel2anki 可以用视觉小说解包文件生成精美的带例句、语音的 Anki 卡片，Yomichan 作为经典好用的日语学习工具也能与 Anki 联动快速添加卡片。为了适应自己的工作流，我在本项目中加入了类似的 Anki 功能。Yomichan 作者开发的 [AnkiConnect](https://ankiweb.net/shared/info/2055492159) 插件使此功能得以实现，感恩大佬。

使用的第三方工具与参考资料：

* 形态分析：[taku910/mecab](https://github.com/taku910/mecab) 的 .Net 移植版本 [kekyo/MeCab.DotNet](https://github.com/kekyo/MeCab.DotNet)
* ChatGPT：[OkGoDoIt/OpenAI-API-dotnet](https://github.com/OkGoDoIt/OpenAI-API-dotnet)
* [前端页面](https://github.com/ks233/ja-learner-webview)：[WebView2 控件](https://www.nuget.org/packages/Microsoft.Web.WebView2)，Vite + Vue
* 单词搜索：[MOJi 辞書](https://www.mojidict.com/)
* 谷歌翻译：参考了 [FilipePS/Traduzir-paginas-web](https://github.com/FilipePS/Traduzir-paginas-web) 的 API 调用方式
* Anki：[AnkiConnect](https://ankiweb.net/shared/info/2055492159)
* 其它参考资源：[taishi-i/awesome-japanese-nlp-resources](https://github.com/taishi-i/awesome-japanese-nlp-resources)

## 贡献者

<!-- ALL-CONTRIBUTORS-LIST:START - Do not remove or modify this section -->
<!-- prettier-ignore-start -->
<!-- markdownlint-disable -->

<table>
  <tbody>
    <tr>
      <td align="center" valign="top" width="14.28%"><a href="https://ks233.github.io/"><img src="https://avatars.githubusercontent.com/u/38981529?v=4?s=100" width="100px;" alt="ks233"/><br /><sub><b>ks233</b></sub></a><br /><a href="#code-ks233" title="Code">💻</a></td>
      <td align="center" valign="top" width="14.28%"><a href="https://github.com/shaka0919"><img src="https://avatars.githubusercontent.com/u/17539962?v=4?s=100" width="100px;" alt="Harvey Wang"/><br /><sub><b>Harvey Wang</b></sub></a><br /><a href="#code-shaka0919" title="Code">💻</a></td>
      <td align="center" valign="top" width="14.28%"><a href="http://lgbt.sh"><img src="https://avatars.githubusercontent.com/u/38471793?v=4?s=100" width="100px;" alt="Mikka"/><br /><sub><b>Mikka</b></sub></a><br /><a href="#infra-cvyl" title="Infrastructure (Hosting, Build-Tools, etc)">🚇</a></td>
    </tr>
  </tbody>
</table>

<!-- markdownlint-restore -->
<!-- prettier-ignore-end -->

<!-- ALL-CONTRIBUTORS-LIST:END -->
