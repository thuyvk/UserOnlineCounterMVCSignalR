using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UserOnlineCounter.Startup))]
namespace UserOnlineCounter
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}