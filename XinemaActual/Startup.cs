using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XinemaActual.Startup))]
namespace XinemaActual
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
