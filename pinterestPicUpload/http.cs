using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PinBot
{
    
    public class http
    {
        
        static bool debug = false;

        public static string POST(string url, string referer, string contentType, List<byte[]> data, List<string> headers, CookieContainer c, WebProxy proxy, string accept = "*/*")
        {
            try
            {

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                if (proxy != null)
                {
                    req.Proxy = proxy;
                    req.PreAuthenticate = true;
                    req.Timeout = 100000;
                }

                //req.Timeout = 15000;
                req.ContentType = contentType;//"multipart/form-data; boundary=" + boundary;
                req.Method = "POST";
                req.KeepAlive = true;
                req.Referer = referer;
                req.AllowAutoRedirect = true;
                req.UserAgent = ("Mozilla/5.0 (Windows NT 6.1; WOW64; rv:30.0) Gecko/20100101 Firefox/30.0");
                req.Accept = accept;
                if (headers != null)
                {
                    foreach (string s in headers)
                    {
                        req.Headers.Add(s);
                    }
                }
                req.CookieContainer = c == null ? new CookieContainer() : c;
                req.ServicePoint.Expect100Continue = false;
                ServicePointManager.Expect100Continue = false;
                req.AutomaticDecompression = DecompressionMethods.GZip;

                Stream stream = req.GetRequestStream();
                foreach (byte[] b in data)
                {
                    stream.Write(b, 0, b.Length);
                }
                stream.Close();

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(res.GetResponseStream());
                string RS = reader.ReadToEnd();

                reader.Close();
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

                writeLog(RS);

                return RS;
            }
            catch (Exception ex){
                if (debug) { MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace); }
                string err = "[[[ERROR]]]\n\n" + ex.StackTrace + "\n\n" + ex.Message;
                writeLog(err);
                return err;
            }
        }
        public static string GET(string url, string referer, CookieContainer c, List<string> headers, WebProxy proxy, string accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8")
        {
            try
            {
                
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                if (proxy != null)
                {
                    req.Proxy = proxy;
                    req.PreAuthenticate = true;
                    req.Timeout = 60000;
                }
                
                //req.Timeout = 15000;
                req.CookieContainer = c == null ? new CookieContainer() : c;
                req.Referer = referer;
                req.Accept = accept;
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

                writeLog(RS);

                return RS;
            }
            catch (Exception ex)
            {
                if (debug) { MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace); }
                string err = "[[[ERROR]]]\n\n" + ex.StackTrace + "\n\n" + ex.Message;
                writeLog(err);
                return err;
            }
        }

        public static string getBoundary()
        {
            return "----------------------------" + DateTime.Now.Ticks.ToString("x");
        }


        private static bool writing;
        private static void writeLog(string s)
        {
            while (writing)
                Thread.Sleep(200);
            writing = true;
            try
            {
                string file = "./debug.txt";
                File.AppendAllText(file, s);
                File.AppendAllText(file, "\r\n\r\n======================\r\n\r\n");
            }
            catch { }
            writing = false;
        }

    }
}
