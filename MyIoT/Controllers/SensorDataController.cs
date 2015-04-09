using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace MyIoT.Controllers
{
    public class SensorDataController : Controller
    {
        [HttpPost]
        public void Index()
        {
            string data = string.Empty;
            //Convert payload to a string
            using (StreamReader reader = new StreamReader(Request.InputStream, Encoding.UTF8))
                data = reader.ReadToEnd();

            string message = string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), data);

            //push incoming data out to any connected browsers
            IHubConnectionContext<dynamic> clients = GlobalHost.ConnectionManager.GetHubContext<SensorDataHub>().Clients;
            clients.All.addNewMessageToPage(message);



        }
    }
}