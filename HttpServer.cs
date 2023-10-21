using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ja_learner
{
    internal class HttpServer
    {
        private static string _rootFolder = Directory.GetCurrentDirectory() + @"\dist";
        private static HttpListener _httpListener;
        public static void StartServer()
        {
            port = FindAvailablePort();
            _httpListener = new HttpListener();
            _httpListener.Prefixes.Add($"http://localhost:{port}/"); // 服务器监听的URL和端口号

            proxyDict["/mojiapi"] = "https://api.mojidict.com";
            proxyDict["/googletrans"] = "https://clients5.google.com/translate_a";

            Task.Run(() =>
            {
                try
                {
                    _httpListener.Start();

                    while (_httpListener.IsListening)
                    {
                        var context = _httpListener.GetContext();
                        Task.Run(() => HandleRequest(context));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            });
        }

        private static int port;

        public static int Port
        {
            get { return port; }
        }


        static int FindAvailablePort()
        {
            int port = 8080;
            while (IsPortInUse(port) && port < 65536)
            {
                port++;
            }
            return port;
        }

        static bool IsPortInUse(int port)
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] activeTcpListeners = properties.GetActiveTcpListeners();
            IPEndPoint[] activeUdpListeners = properties.GetActiveUdpListeners();

            foreach (IPEndPoint endPoint in activeTcpListeners)
            {
                if (endPoint.Port == port)
                {
                    return true;
                }
            }

            foreach (IPEndPoint endPoint in activeUdpListeners)
            {
                if (endPoint.Port == port)
                {
                    return true;
                }
            }

            return false;
        }

        private static Dictionary<string, string> proxyDict = new Dictionary<string, string>();

        private static bool StartsWithProxy(string url, out string proxyName)
        {
            foreach(string key in proxyDict.Keys)
            {
                if (url.StartsWith(key))
                {
                    proxyName = key;
                    return true;
                }
            }
            proxyName = "";
            return false;
        }

        private static async void HandleRequest(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;

            // 获取请求的文件路径
            var localPath = request.Url.LocalPath.TrimStart('/');
            var filePath = string.IsNullOrEmpty(localPath) ? Path.Combine(_rootFolder, "index.html") : Path.Combine(_rootFolder, localPath);

            string url = request.Url.LocalPath;

            string proxyName = proxyDict.Keys.FirstOrDefault(key => url.StartsWith(key));
            // 代理
            if (proxyName != null)
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    // string apiUrl = url.Replace("/mojiapi", "https://api.mojidict.com");

                    HttpResponseMessage r;
                    string apiUrl = url.Replace(proxyName, proxyDict[proxyName]);
                    if (request.HttpMethod == "GET")
                    {
                        r = await httpClient.GetAsync(request.RawUrl.Replace(proxyName, proxyDict[proxyName]));
                    }
                    else
                    {
                        string json = "";
                        using (StreamReader reader = new StreamReader(request.InputStream))
                        {
                            json = reader.ReadToEnd();
                        }
                        httpClient.DefaultRequestHeaders.Clear();
                        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                        r = await httpClient.PostAsync(apiUrl, content);
                    }
                    // 将代理请求的响应返回给客户端
                    response.StatusCode = (int)r.StatusCode;
                    response.ContentType = r.Content.Headers.ContentType?.ToString();
                    byte[] buffer = await r.Content.ReadAsByteArrayAsync();
                    response.ContentLength64 = buffer.Length;
                    response.OutputStream.Write(buffer, 0, buffer.Length);
                }
                response.Close();
            }
            else if (File.Exists(filePath))
            {
                try
                {
                    // 发送文件内容作为响应
                    using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        response.ContentLength64 = fs.Length;
                        response.ContentType = GetMimeType(filePath);
                        fs.CopyTo(response.OutputStream);
                    }
                }
                catch (Exception ex)
                {
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Close();
                    return;
                }

                response.StatusCode = (int)HttpStatusCode.OK;
                response.Close();
            }
            else
            {
                // 请求为路由
                var vueIndexPath = Path.Combine(_rootFolder, "index.html");

                if (File.Exists(vueIndexPath))
                {
                    try
                    {
                        // 读取Vue应用的入口点文件
                        var vueIndexContent = File.ReadAllText(vueIndexPath);

                        // 设置响应的Content-Type
                        response.ContentType = "text/html";

                        // 发送Vue应用的入口点文件内容作为响应
                        var buffer = Encoding.UTF8.GetBytes(vueIndexContent);
                        response.ContentLength64 = buffer.Length;
                        response.OutputStream.Write(buffer, 0, buffer.Length);
                    }
                    catch (Exception ex)
                    {
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        response.Close();
                        return;
                    }

                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Close();
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Close();
                }
            }
        }

        private static string GetMimeType(string filePath)
        {
            // 根据文件扩展名获取相应的MIME类型
            string mimeType;
            var extension = Path.GetExtension(filePath).ToLower();

            switch (extension)
            {
                case ".html":
                    mimeType = "text/html";
                    break;
                case ".css":
                    mimeType = "text/css";
                    break;
                case ".js":
                    mimeType = "application/javascript";
                    break;
                case ".png":
                    mimeType = "image/png";
                    break;
                case ".jpg":
                case ".jpeg":
                    mimeType = "image/jpeg";
                    break;
                case ".gif":
                    mimeType = "image/gif";
                    break;
                case ".svg":
                    mimeType = "image/svg+xml";
                    break;
                default:
                    mimeType = "application/octet-stream";
                    break;
            }

            return mimeType;
        }
    }

}
