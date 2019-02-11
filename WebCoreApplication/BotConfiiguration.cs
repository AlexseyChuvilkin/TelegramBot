using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication
{
    public class BotConfiguration
    {
        public BotConfiguration(string botToken, string socks5Host, int socks5Port)
        {
            BotToken = botToken ?? throw new ArgumentNullException(nameof(botToken));
            Socks5Host = socks5Host ?? throw new ArgumentNullException(nameof(socks5Host));
            Socks5Port = socks5Port;
        }

        public string BotToken { get; set; }
        public string Socks5Host { get; set; }
        public int Socks5Port { get; set; }
    }
}