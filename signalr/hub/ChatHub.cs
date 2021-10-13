using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyFly.signalr.hub
{
    public class ChatHub : Hub
    {
        public void send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }
    }
}