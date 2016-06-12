using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SD_Ambience.Startup))]
namespace SD_Ambience
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
