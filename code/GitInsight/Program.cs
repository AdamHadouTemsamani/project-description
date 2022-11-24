using LibGit2Sharp;
using System.Linq;
using System;


public static class Program 
{
    private static  IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }
}