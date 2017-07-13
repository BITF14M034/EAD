using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EAD_CMS.Startup))]
namespace EAD_CMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
