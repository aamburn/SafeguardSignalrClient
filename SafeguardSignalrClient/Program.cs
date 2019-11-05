using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;
using OneIdentity.SafeguardDotNet;
using OneIdentity.SafeguardDotNet.Event;
using OneIdentity.SafeguardDotNet.GuiLogin;
using HttpWebRequest = System.Net.HttpWebRequest;

namespace SafeguardSignalrClient
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: SafeguardSignalrClient [URL] [THUMBPRINT] [EVENT]");
                Environment.Exit(1);
            }

            string url = args[0];
            string thumbprint = args[1];
            string sgEvent = args[2];

//          uncomment to use GUI authentication
//          var connection = LoginWindow.Connect(url);

            var connection = Safeguard.Connect(url, thumbprint, 3,true);
            
            
            var listener = connection.GetEventListener();
            listener.RegisterEventHandler(sgEvent, Handler);

            listener.Start();
            Console.WriteLine("Listening for AssetUpdated events...\n...\n...\npress CTRL+C to exit at any time");

            while (true)
            {

            }
        }

        private static void Handler(string eventname, string eventbody)
        {
            Console.WriteLine(eventname);
            Console.WriteLine(eventbody);
        }


    }
}