using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TemperatureMonitor.Startup))]
namespace TemperatureMonitor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
