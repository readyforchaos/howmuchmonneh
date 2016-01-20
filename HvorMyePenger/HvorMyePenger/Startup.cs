using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HvorMyePenger.Startup))]
namespace HvorMyePenger
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
