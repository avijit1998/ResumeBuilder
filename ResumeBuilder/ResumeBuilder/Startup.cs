using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ResumeBuilder.Startup))]
namespace ResumeBuilder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}


