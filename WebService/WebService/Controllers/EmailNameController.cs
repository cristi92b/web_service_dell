using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebService
{
    public class EmailNameController
    {
        private readonly string responseStrHeader = "<!DOCTYPE html><html><body>";
        private readonly string responseStrFooter = "</body></html>";

        private HttpListener listener;
        private Dictionary<string, string> data;

        public EmailNameController(long portNumber)
        {
            data = new Dictionary<string, string>();
            listener = new HttpListener();
            listener.Prefixes.Add("http://*:" + portNumber + "/");
        }

        public void StartListening()
        {
            try
            {
                listener.Start();
                ListenAsync();
            }
            catch (HttpListenerException hlex)
            {
                Console.WriteLine("Exception occurred: " + hlex);
            }
        }

        public void StopListening()
        {
            listener.Close();
        }

        private async Task ListenAsync()
        {
            while (true)
            {
                var context = await listener.GetContextAsync();
                Console.WriteLine("Client connected");
                await Task.Factory.StartNew(() => ProcessRequest(context));
            }
        }

        private void ProcessRequest(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            string responseString = "";
            string email = "";
            string name = "";
            bool isPost = false;
            if (request.HttpMethod == "POST")
            {
                email = request.QueryString["email"];
                name = request.QueryString["name"];
                isPost = true;
            }
            else if(request.HttpMethod == "GET")
            {
                email = request.QueryString["email"];
            }
            if (!isPost)
            {
                bool emailFound = true; ;
                try
                {
                    name = data[email];
                }
                catch(Exception ex)
                {
                    emailFound = false;
                }
                if(emailFound)
                {
                    responseString = "Found Customer: Name = " + name + " Email = " + email;
                }
                else
                {
                    responseString = "Could not find customer with email address = " + email;
                }
            }
            else
            {
                bool success = true;
                try
                {
                    data[email] = name;
                }
                catch(Exception ex)
                {
                    success = false;
                }
                if(success)
                {
                    responseString = "Successfully updated customer data: Name = " + name + " Email = " + email;
                }
                else
                {
                    responseString = "Failed to update customer data customer data: Name = " + name + " Email = " + email;
                }
            }
            HttpListenerResponse response = context.Response;
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseStrHeader + responseString + responseStrFooter);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();
        }

    }
}
