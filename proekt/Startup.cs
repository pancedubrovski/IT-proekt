using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(proekt.Startup))]
namespace proekt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
