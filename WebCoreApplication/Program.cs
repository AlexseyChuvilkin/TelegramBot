using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using WebCoreApplication.Models;

namespace WebCoreApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
        }
        public static IWebHost BuildWebHost(string[] args) => WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build();

        private static void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e) => Data.AddLog(e.Exception.ToString());
    }
}
