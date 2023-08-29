using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace HamiDiabet.ClassCollection
{
    public class Method
    {
        public static string exc(WebException ex)
        {
            var stream = ex.Response.GetResponseStream();
            byte[] buf;
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                buf = ms.ToArray();
            }
            var x = Encoding.UTF8.GetString(buf);
            return x;
        }
    }
}