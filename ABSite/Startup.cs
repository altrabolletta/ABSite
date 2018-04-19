using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ABSite.Startup))]
namespace ABSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
