using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApiWithMVC.Startup))]
namespace WebApiWithMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
