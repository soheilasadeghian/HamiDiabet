using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HamiDiabet.Models
{
    public class ArticlesResult
    {
        public Result result { get; set; }
        public List<Article> articles { get; set; }
    }
}