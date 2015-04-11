using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;

namespace MyIoT.Controllers
{
    public class SensorDataController : Controller
    {
        public void Index()
        {
            var queryString = HttpContext.Request.RawUrl;
            int index = queryString.IndexOf('?');
            queryString = index > 0 ? queryString.Substring(queryString.IndexOf('?') + 1) : string.Empty;

            pushMessage(queryString);
        }

        [HttpPost]
        [ActionName("Index")]
        public void Index_post()
        {
            var data = string.Empty;
            //Convert payload to a string
            using (var reader = new StreamReader(Request.InputStream, Encoding.UTF8))
                data = reader.ReadToEnd();

            pushMessage(data);
        }

        private static void pushMessage(string message)
        {
            message = string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), message);
            //push incoming data out to any connected browsers
            var clients = GlobalHost.ConnectionManager.GetHubContext<SensorDataHub>().Clients;
            clients.All.addNewMessageToPage(message);
        }
    }
}