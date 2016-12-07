using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TamTam.Startup))]
namespace TamTam
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
