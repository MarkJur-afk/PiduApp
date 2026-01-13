using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PiduApp.Startup))]
namespace PiduApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
