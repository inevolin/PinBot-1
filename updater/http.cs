using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace updater
{
    public class http
    {

        public static string GET(string url, string referer, CookieContainer c, List<string> headers)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

    

                //req.Timeout = 15000;
                req.CookieContainer = c;
                req.Referer = referer;
                req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                req.UserAgent = ("Mozilla/5.0 (Windows NT 6.1; WOW64; rv:30.0) Gecko/20100101 Firefox/30.0");
                if (headers != null)
                {
                    foreach (string s in headers)
                        req.Headers.Add(s);
                }
                req.AllowAutoRedirect = true;
                req.KeepAlive = true;
                req.AutomaticDecompression = DecompressionMethods.GZip;
                ServicePointManager.Expect100Continue = false;
                req.ServicePoint.Expect100Continue = false;
                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                string RS = reader.ReadToEnd();
                response.Close();
                req.Abort();
                stream.Close();
                return RS;
            }
            catch (WebException ex)
            {
                HttpWebResponse objResponse = ex.Response as HttpWebResponse;
                StreamReader reader = new StreamReader(objResponse.GetResponseStream());
                string RS = "[[[ERROR]]]\n\n" + reader.ReadToEnd() + "\n\n" + objResponse.ResponseUri + "\n";

                for (int i = 0; i < objResponse.Headers.Count; ++i)
                    RS += ("\n" + objResponse.Headers.Keys[i] + ": " + objResponse.Headers[i]);

          

                return RS;
            }
            catch (Exception ex)
            {
                return "ERROR";
            }
        }

        public static string GET__(string url, string referer, CookieContainer c, List<string> headers)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.CookieContainer = c;
                req.Referer = referer;
                req.Accept = "application/json, text/javascript, */*; q=0.01"; 
                req.UserAgent = ("Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.153 Safari/537.36");
                if (headers != null)
                {
                    foreach (string s in headers)
                        req.Headers.Add(s);
                }
                req.AllowAutoRedirect = false;
                req.KeepAlive = true;
                req.AutomaticDecompression = DecompressionMethods.GZip;
                ServicePointManager.Expect100Continue = false;
                req.ServicePoint.Expect100Continue = false;
                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                string RS = reader.ReadToEnd();
                response.Close();
                req.Abort();
                stream.Close();
                return RS;
            }
            catch (Exception e) { Console.WriteLine(e.Message.ToString()); return "ERROR"; }
        }

        public static string getBoundary()
        {
            return "----------------------------" + DateTime.Now.Ticks.ToString("x");
        }

    }
}
