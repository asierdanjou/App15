using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(App15.Backend.Startup))]
namespace App15.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
