using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
	        int port = 5001;
	        Console.Title = $"IdentityServer4 - WebApi : port: {port}";

			var host = new WebHostBuilder()
                .UseKestrel()
				.UseUrls($"http://localhost:{port}")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
