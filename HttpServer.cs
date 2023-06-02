using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ja_learner
{
    internal class HttpServer
    {
        private static string _rootFolder = @"E:\Github\ja-learner-webview\dist";
        private static HttpListener _httpListener;
        public static void StartServer()
        {
            _httpListener = new HttpListener();
            _httpListener.Prefixes.Add("http://localhost:8080/"); // 服务器监听的URL和端口号

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

        private static void HandleRequest(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;

            // 获取请求的文件路径
            var localPath = request.Url.LocalPath.TrimStart('/');
            var filePath = string.IsNullOrEmpty(localPath) ? Path.Combine(_rootFolder, "index.html") : Path.Combine(_rootFolder, localPath);

            if (File.Exists(filePath))
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
