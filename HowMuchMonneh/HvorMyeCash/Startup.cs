using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HvorMyeCash.Startup))]
namespace HvorMyeCash
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
