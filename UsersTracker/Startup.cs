using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UsersTracker.Startup))]
namespace UsersTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
