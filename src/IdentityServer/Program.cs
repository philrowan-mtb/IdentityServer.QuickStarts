using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace BasicSetup
{
    public class Program
    {
        public static void Main(string[] args)
        {
			int port = 5000;
	        Console.Title = $"IdentityServer4 - ID Server : port: {port}";

			var host = new WebHostBuilder()
                .UseKestrel()
	            .UseUrls($"http://localhost:{port}")
				.UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
