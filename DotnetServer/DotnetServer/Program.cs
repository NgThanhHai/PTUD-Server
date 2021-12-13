using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.AspNetCore;
using DotnetServer.Services;

namespace DotnetServer
{
    public class Program
    {
    
        public static void Main(string[] args)
        {
       
            var host = CreateWebHostBuilder(args).Build();
            host.Run();
        }

        public static Microsoft.AspNetCore.Hosting.IWebHostBuilder
        CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args).
            UseStartup<Startup>();
 
}
}
