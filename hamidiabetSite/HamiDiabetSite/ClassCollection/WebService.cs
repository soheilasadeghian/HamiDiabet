using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using HamiDiabet.Models;
using HamiDiabet.ClassCollection;

namespace HamiDiabet.ClassCollection
{
    public class WebService
    {
        public static class Core
        {
            public static string CallMethod(string method, Dictionary<string, string> Parameters,bool Authorization)
            {
                string webServiceURL = "http://localhost:55097/" + method;
                byte[] dataStream = CreateHttpRequestData(Parameters);
                
                WebRequest request = WebRequest.Create(webServiceURL);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                if (Authorization)
                {
                    request.Headers.Add("Authorization", "Bearer " + HttpContext.Current.Session["access_token"]);
                }

                Stream stream = request.GetRequestStream();
                stream.Write(dataStream, 0, dataStream.Length);
                stream.Close();
                WebResponse response = request.GetResponse();
                Stream respStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(respStream);

                string json = reader.ReadToEnd();
                return json;

            }
            private static byte[] CreateHttpRequestData(Dictionary<string, string> dic)
            {
                StringBuilder sbParameters = new StringBuilder();
                foreach (string param in dic.Keys)
                {
                    sbParameters.Append(param);//key => parameter name
                    sbParameters.Append('=');
                    sbParameters.Append(dic[param]);//key value
                    sbParameters.Append('&');
                }
                sbParameters.Remove(sbParameters.Length - 1, 1);

                UTF8Encoding encoding = new UTF8Encoding();

                return encoding.GetBytes(sbParameters.ToString());

            }
        }
        
    }
}