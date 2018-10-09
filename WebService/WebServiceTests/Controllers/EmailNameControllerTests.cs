using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace WebService.Tests
{
    [TestClass()]
    public class EmailNameControllerTests
    {
        [TestMethod()]
        public void EmailNameControllerTest()
        {

        }

        [TestMethod()]
        public void StartListeningTest()
        {
            EmailNameController controller = new EmailNameController(8080);
            controller.StartListening();

            var url = "http://localhost:8080";
            var postClient1 = new WebClient();
            postClient1.QueryString.Add("name", "John Smith");
            postClient1.QueryString.Add("email", "john.smith@gmail.com");
            var postClient1data = postClient1.UploadValues(url, "POST", postClient1.QueryString);
            var response1String = UnicodeEncoding.UTF8.GetString(postClient1data);

            Assert.IsTrue(response1String.Contains("Successfully updated customer data: Name = John Smith Email = john.smith@gmail.com"));

            var postClient2 = new WebClient();
            postClient2.QueryString.Add("name", "Alice");
            postClient2.QueryString.Add("email", "alice123@gmail.com");
            var postClient2data = postClient2.UploadValues(url, "POST", postClient2.QueryString);
            var response2String = UnicodeEncoding.UTF8.GetString(postClient2data);

            Assert.IsTrue(response2String.Contains("Successfully updated customer data: Name = Alice Email = alice123@gmail.com"));

            var getClient1 = new WebClient();
            getClient1.QueryString.Add("email", "john.smith@gmail.com");
            var getClient1data = getClient1.DownloadString(url);

            Assert.IsTrue(getClient1data.Contains("Found Customer: Name = John Smith Email = john.smith@gmail.com"));

            var postClient3 = new WebClient();
            postClient3.QueryString.Add("name", "John");
            postClient3.QueryString.Add("email", "john.smith@gmail.com");
            var postClient3data = postClient3.UploadValues(url, "POST", postClient3.QueryString);
            var response3String = UnicodeEncoding.UTF8.GetString(postClient3data);

            Assert.IsTrue(response3String.Contains("Successfully updated customer data: Name = John Email = john.smith@gmail.com"));

            var getClient2 = new WebClient();
            getClient2.QueryString.Add("email", "john.smith@gmail.com");
            var getClient2data = getClient2.DownloadString(url);

            Assert.IsTrue(getClient2data.Contains("Found Customer: Name = John Email = john.smith@gmail.com"));

            /*
            WebRequest request = WebRequest.Create("http://localhost:8080");
            request.Method = "POST";
            request.q
            request.Headers.Add("name", "John Smith");
            request.Headers.Add("email", "john.smith@gmail.com");
            
            request.ContentLength = 0;

            WebResponse response = request.GetResponse();
            Stream responseStream =  response.GetResponseStream();
            byte[] buffer = new byte[1024];
            responseStream.Read(buffer, 0, 1023);

            string responseStr = System.Text.Encoding.UTF8.GetString(buffer);

            string[] postResult = response.Headers.AllKeys;
            */
            //Assert.AreEqual(postResult, "");
            /*
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            Console.WriteLine(responseFromServer);
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();
            */

        }
    }
}