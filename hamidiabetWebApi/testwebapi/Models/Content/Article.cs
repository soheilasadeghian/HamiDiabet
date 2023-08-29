using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HamiDiabetWebApi.Models.Content
{
    public class Article
    {
        public string subject { get; set; }
        public string registerDate { get; set; }
        public List<ArticleDetail> articleDetail { get; set; }
    }
}