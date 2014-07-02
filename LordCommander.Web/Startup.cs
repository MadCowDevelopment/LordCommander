using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(LordCommander.Web.Startup))]

namespace LordCommander.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var configuration = new HubConfiguration();
            configuration.EnableDetailedErrors = true;
            app.MapSignalR(configuration);
        }
    }
}
