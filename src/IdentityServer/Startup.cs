using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IdentityServer4;

namespace BasicSetup
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
	        services.AddIdentityServer()
		        .AddTemporarySigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
		        .AddInMemoryApiResources(Config.GetApiResources())
		        .AddInMemoryClients(Config.GetClients())
		        .AddTestUsers(Config.GetUsers());

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseDeveloperExceptionPage();

			app.UseIdentityServer();

            app.UseGoogleAuthentication(new GoogleOptions
            {
                AuthenticationScheme = "Google",
                DisplayName = "Google",
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,

                ClientId = "414813138911-vgb85nv4r9s13j2uerf6n37c3od06735.apps.googleusercontent.com",
                ClientSecret = "7AWal9vuZWBLwChQFfcG7Gcu"
            });

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
