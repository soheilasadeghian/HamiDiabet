using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HamiDiabetWebApi.ClassCollection
{
    public class Method
    {
        public static string md5(string sPassword)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(sPassword);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }
        public static string convertPersianNumberToEnglishNumber(string str)
        {
            char[] temp = str.ToCharArray();
            for (int i = 0; i < temp.Length; i++)
            {
                switch (Convert.ToInt32(temp[i]).ToString())
                {
                    case "1632":
                        temp[i] = '0';
                        break;
                    case "1633":
                        temp[i] = '1';
                        break;
                    case "1634":
                        temp[i] = '2';
                        break;
                    case "1635":
                        temp[i] = '3';
                        break;
                    case "1636":
                        temp[i] = '4';
                        break;
                    case "1637":
                        temp[i] = '5';
                        break;
                    case "1638":
                        temp[i] = '6';
                        break;
                    case "1639":
                        temp[i] = '7';
                        break;
                    case "1640":
                        temp[i] = '8';
                        break;
                    case "1641":
                        temp[i] = '9';
                        break;

                    case "1776":
                        temp[i] = '0';
                        break;
                    case "1777":
                        temp[i] = '1';
                        break;
                    case "1778":
                        temp[i] = '2';
                        break;
                    case "1779":
                        temp[i] = '3';
                        break;
                    case "1780":
                        temp[i] = '4';
                        break;
                    case "1781":
                        temp[i] = '5';
                        break;
                    case "1782":
                        temp[i] = '6';
                        break;
                    case "1783":
                        temp[i] = '7';
                        break;
                    case "1784":
                        temp[i] = '8';
                        break;
                    case "1785":
                        temp[i] = '9';
                        break;
                    default:

                        break;

                }
            }

            return new string(temp);


            // return value.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("v", "7").Replace("۸", "8").Replace("۹", "9");
        }

        public static bool IsMobile(string value)
        {
            bool c1 = value.Length == 11;
            bool c2 = value.StartsWith("09");
            bool c3 = value.Any(c => c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9' || c == '0');
            return c1 &&
                c2 &&
                c3;
        }
    }
}