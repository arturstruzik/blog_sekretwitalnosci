using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(blog_akademiazdrowia.Startup))]
namespace blog_akademiazdrowia
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
