using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hnatob.WebUI.Startup))]
namespace Hnatob.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
