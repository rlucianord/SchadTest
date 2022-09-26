using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Schad.Web.Startup))]
namespace Schad.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
