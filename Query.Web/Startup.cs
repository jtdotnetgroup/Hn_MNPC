using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Query.Web.Startup))]
namespace Query.Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
