using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace ApiClient.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int port = 5002;
            Console.Title = $"IdentityServer4 - Client Server : port: {port}";

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
