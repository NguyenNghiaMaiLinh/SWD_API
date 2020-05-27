using Microsoft.Extensions.Configuration;

namespace DemoApp.Core.Configs
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public static AppSettings Instance { get; set; }
        public static IConfiguration Configs { get; set; }
    }
}
