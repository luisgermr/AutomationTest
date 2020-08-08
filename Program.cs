using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
namespace Examples.System.Net
{
    public class WebRequestTest
    {
        static void Request(string alpha2Code)
        {
            try
            {
                //This lines creates the request and response obtained from the request.
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://restcountries.eu/rest/v2/alpha/"+ alpha2Code);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                //This line print the status code of the response
                Console.WriteLine(((HttpWebResponse)response).StatusCode);

                //This lines read the data obtained from the request and print it.
                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    Console.WriteLine(responseFromServer);
                }
                response.Close();
            }
            catch (WebException e)
            {
                // In case the function got an exception, shows a message to the tester indicating that something went wrong. Also show the Status Code and Description.
                Console.WriteLine("This test is expected to be successful, must be reported if got an exception." +
                                    "\n\nException Message :" + e.Message);
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    Console.WriteLine("Status Code : {0}", ((HttpWebResponse)e.Response).StatusCode);
                    Console.WriteLine("Status Description : {0}", ((HttpWebResponse)e.Response).StatusDescription);
                }
            }

            Console.WriteLine("------------------------------------------------------------------");
        }

        static void BadRequest(string alpha2Code)
        {
            try
            {
                //This lines creates the request and response obtained from the request.
                HttpWebRequest badRequest = (HttpWebRequest)WebRequest.Create("https://restcountries.eu/rest/v2/alpha/"+ alpha2Code);
                HttpWebResponse badResponse = (HttpWebResponse)badRequest.GetResponse();

                //This line print the status code of the response
                Console.WriteLine(((HttpWebResponse)badResponse).StatusCode);

                //This lines read the data obtained from the request and print it.
                using (Stream dataStream = badResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    Console.WriteLine(responseFromServer);
                }

                badResponse.Close();
            }
            catch (WebException e)
            {
                // In case the function got an exception, shows a message to the tester indicating that something went wrong. Also show the Status Code and Description.
                Console.WriteLine("This test is expected to throw a Exception." +
                                    "\n\nException Message :" + e.Message);
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    Console.WriteLine("Status Code : {0}", ((HttpWebResponse)e.Response).StatusCode);
                    Console.WriteLine("Status Description : {0}", ((HttpWebResponse)e.Response).StatusDescription);
                }
            }
            Console.WriteLine("------------------------------------------------------------------");
        }

        static void Post()
        {
            //This lines create the post request and specifies that the content type to use is json.
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://restcountries.eu/rest/v2/alpha/post");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            //This lines create the json to post.
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                 string json = "{\"name\":\"Test Country\"," +
                                  "\"alpha2code\":\"TC\"," + 
                                  "\"alpha3code\":\"TCY\"}";
            streamWriter.Write(json);
            }
            
            //This lines should show the response result of the request.
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }
        public static void Main()
        {
            Console.WriteLine("First Test Scenario: Get info of US, DE and GB");
            Console.WriteLine("");
            Request("us");
            Request("de");
            Request("gb");

            Console.WriteLine("");
            Console.WriteLine("Second Test Scenario: Get info of inexistent country (xx)");
            Console.WriteLine("");

            BadRequest("xx");

            
            Console.WriteLine("");
            Console.WriteLine("Third Test Scenario: Simulated Post method (read the comment code)");
            Console.WriteLine("");
            //Post();

            Console.ReadKey();
        }
    }
}
