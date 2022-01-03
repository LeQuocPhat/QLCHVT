using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QLCHVT.Startup))]
namespace QLCHVT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
