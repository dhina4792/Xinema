using Microsoft.Owin;
using Owin;
using System.Data.Entity;
using XinemaActual.DAL;

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
