using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PizzaDotNET.Startup))]
namespace PizzaDotNET
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
