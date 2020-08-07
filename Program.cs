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
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://restcountries.eu/rest/v2/alpha/"+ alpha2Code);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Console.WriteLine(((HttpWebResponse)response).StatusCode);
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
                HttpWebRequest badRequest = (HttpWebRequest)WebRequest.Create("https://restcountries.eu/rest/v2/alpha/"+ alpha2Code);
                HttpWebResponse badResponse = (HttpWebResponse)badRequest.GetResponse();
                Console.WriteLine(((HttpWebResponse)badResponse).StatusDescription);
                badResponse.Close();
            }
            catch (WebException e)
            {
                Console.WriteLine("This test is expected to throw a Exception." +
                                    "\n\nException Message :" + e.Message);
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    Console.WriteLine("Status Code : {0}", ((HttpWebResponse)e.Response).StatusCode);
                    Console.WriteLine("Status Description : {0}", ((HttpWebResponse)e.Response).StatusDescription);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("------------------------------------------------------------------");
        }

        static void Post()
        {
            //var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://restcountries.eu/rest/v2/alpha/post");
            //httpWebRequest.ContentType = "application/json";
            //httpWebRequest.Method = "POST";
            //
            //using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            //{
            //    string json = new JavaScriptSerializer().Serialize(new
            //    {
            //            name = "Test Country", 
            //            alpha2_code= "TC", 
            //            alpha3_code= "TCY"
            //    });
            //
            //    streamWriter.Write(json);
            //}
            //
            //var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            //{
            //    var result = streamReader.ReadToEnd();
            //}
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
            Console.WriteLine("Third Test Scenario: Simulating Post method (read the code)");
            Console.WriteLine("");

            //Post();

            Console.ReadKey();
        }
    }
}
