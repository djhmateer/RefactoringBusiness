using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RefactoringCSharp.Startup))]
namespace RefactoringCSharp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
