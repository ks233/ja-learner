using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace ja_learner
{
    internal static class Program
    {
        public static AppSettingOptions APP_SETTING;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            HostApplicationBuilder builder = Host.CreateApplicationBuilder();
            builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            builder.Services.Configure<AppSettingOptions>(builder.Configuration.GetSection("AppSetting"));

            var host = builder.Build();

            APP_SETTING = host.Services.GetRequiredService<IOptions<AppSettingOptions>>().Value;
            Application.Run(new MainForm());
        }
    }
}