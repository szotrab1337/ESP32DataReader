using ESP32DataReader.Model;
using Fleck;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ESP32DataReader
{
    public partial class MainService : ServiceBase
    {
        private WebSocketServer webSocketServer;
        readonly string serverUri = "ws://192.168.1.100:7890/Reading";

        public MainService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Logger.LogMessage("Service is working.", 1);

            webSocketServer = new WebSocketServer(serverUri);

            webSocketServer.Start(socket =>
            {
                socket.OnOpen = () => ServerOnStart();
                socket.OnClose = () => ServerOnStop();
                socket.OnMessage = message => ServerOnMessage(message);
            });
        }

        protected override void OnStop()
        {
            Logger.LogMessage("Service is not working.", 1);
        }

        private void ServerOnStart()
        {
            Logger.LogMessage("Sever has been created.", 1);
            Logger.LogMessage("Started monitoring - ws://192.168.1.100:7890/Reading.");
        }
        
        private void ServerOnStop()
        {
            Logger.LogMessage("Sever has been stopped.", 1);
        }
        
        private void ServerOnMessage(string message)
        {
            Logger.LogEmpty();
            Logger.LogEmpty();
            Logger.LogMessage("New reading. Raw data: " + message + ".");

            Reading reading = new Reading(message);
            reading.InsertToDB();
        }
    }
}
