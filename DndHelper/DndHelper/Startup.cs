using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DndHelper.Startup))]
namespace DndHelper
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
