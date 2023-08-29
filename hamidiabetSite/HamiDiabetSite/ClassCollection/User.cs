using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using HamiDiabet.Models;
using System.ComponentModel;

namespace HamiDiabet.ClassCollection
{
    public class User
    {
        public static Result SignUp(string name, string family, string cityId, string mobile, string password, bool subscribeNewsletter)
        {
            var result = new Result();
            try
            {
                var dic = new Dictionary<string, string>();
                dic.Add("name", name + "");
                dic.Add("family", family + "");
                dic.Add("cityId", cityId + "");
                dic.Add("mobile", mobile + "");
                dic.Add("password", password + "");
                dic.Add("subscribeNewsletter", subscribeNewsletter + "");

                string json = HamiDiabet.ClassCollection.WebService.Core.CallMethod("user/signup", dic,false);
                json = json.Replace(@"\", string.Empty);
                json = json.Trim().Substring(1, (json.Length) - 2);
                result.code = 0;
                result.message = json;
                return result;
            }
            catch (WebException ex)
            {
                try
                {
                    var str = Method.exc(ex);
                    var resultt = new JavaScriptSerializer().Deserialize<Result>(str);
                    var response = ex.Response as HttpWebResponse;
                    resultt.code = (int)response.StatusCode;
                    return resultt;
                }
                catch (Exception e)
                {
                    result.code = 1000;
                    result.message = e.Message;
                    return result;
                }
            }
        }
        public static Result SignIn(string mobile, string password)
        {
            var result = new Result();
            try
            {
                var dic = new Dictionary<string, string>();
                dic.Add("username", mobile + "");
                dic.Add("password", password + "");
                dic.Add("grant_type", "password" + "");

                string json = WebService.Core.CallMethod("user/signin", dic,false);

                var strSerialize = new JavaScriptSerializer().Deserialize<dynamic>(json);
                HttpContext.Current.Session["access_token"] = strSerialize["access_token"];
                HttpContext.Current.Session["refresh_token"] = strSerialize["refresh_token"];

                result.code = 0;
                result.message = "SUCCESSFULL";

                return result;
            }
            catch (WebException ex)
            {
                try
                {
                    var str = Method.exc(ex);
                    var resultt = new JavaScriptSerializer().Deserialize<Result>(str);
                    var response = ex.Response as HttpWebResponse;
                    resultt.code = (int)response.StatusCode;
                    return resultt;
                }
                catch (Exception e)
                {
                    result.code = 1000;
                    result.message = e.Message;
                    return result;
                }
            }
        }
        public static Result RefreshToken()
        {
            var result = new Result();
            try
            {
                var dic = new Dictionary<string, string>();
                dic.Add("grant_type", "refresh_token" + "");
                dic.Add("refresh_token", HttpContext.Current.Session["refresh_token"] + "");

                string json = WebService.Core.CallMethod("user/signin", dic,false);

                var strSerialize = new JavaScriptSerializer().Deserialize<dynamic>(json);
                HttpContext.Current.Session["access_token"] = strSerialize["access_token"];
                HttpContext.Current.Session["refresh_token"] = strSerialize["refresh_token"];

                result.code = 0;
                result.message = "SUCCESSFULL";

                return result;
            }
            catch (WebException ex)
            {
                try
                {
                    var str = Method.exc(ex);
                    var resultt = new JavaScriptSerializer().Deserialize<Result>(str);
                    var response = ex.Response as HttpWebResponse;
                    resultt.code = (int)response.StatusCode;
                    return resultt;
                }
                catch (Exception e)
                {
                    result.code = 1000;
                    result.message = e.Message;
                    return result;
                }
            }
        }
        public static Result SignOut()
        {
            var result = new Result();
            try
            {
                var dic = new Dictionary<string, string>();
                dic.Add("", "");
                string json = HamiDiabet.ClassCollection.WebService.Core.CallMethod("user/signout", dic, true);

                var strSerialize = new JavaScriptSerializer().Deserialize<dynamic>(json);
                HttpContext.Current.Session["access_token"] = null;
                result.code = 0;
                result.message = "SUCCESSFULL";

                return result;
            }
            catch (WebException ex)
            {
                try
                {
                    var str = Method.exc(ex);
                    var resultt = new JavaScriptSerializer().Deserialize<Result>(str);
                    var response = ex.Response as HttpWebResponse;
                    resultt.code = (int)response.StatusCode;
                    return resultt;
                }
                catch (Exception e)
                {
                    result.code = 1000;
                    result.message = e.Message;
                    return result;
                }
            }
        }
    }
}