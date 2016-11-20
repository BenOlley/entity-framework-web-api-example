using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TemperatureMonitorWebApiClient.Startup))]
namespace TemperatureMonitorWebApiClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
