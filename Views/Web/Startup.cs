using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KarmicEnergy.Web.Startup))]
namespace KarmicEnergy.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
