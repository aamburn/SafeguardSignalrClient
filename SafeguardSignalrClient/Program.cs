using System;
using OneIdentity.SafeguardDotNet;
using OneIdentity.SafeguardDotNet.GuiLogin;

namespace SafeguardSignalrClient
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: SafeguardSignalrClient [URL] [EVENT]");
                Environment.Exit(1);
            }
            
            //extract URL, event name from command line arguments
            string url = args[0];
            string sgEvent = args[1];

            //connect to Safeguard and open login prompt window to prompt for creds
            var connection = LoginWindow.Connect(url);
            
            //create listener and connect to signalr. Assign handler function defined below 
            //to execute when an event is received
            var listener = connection.GetEventListener();
            listener.RegisterEventHandler(sgEvent, Handler);

            //start listener and wait for any input, then stop.
            listener.Start();
            Console.WriteLine($"Listening for {sgEvent} events...\npress any key to exit");
            Console.ReadKey();
            listener.Stop();
        }

        private static void Handler(string eventname, string eventbody)
        {
            Console.WriteLine(eventname);
            Console.WriteLine(eventbody);
        }


    }
}