using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BankProgram.Startup))]
namespace BankProgram
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
