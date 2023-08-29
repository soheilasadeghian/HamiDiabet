using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HamiDiabetWebApi.Models.Content
{
    public class ArticleList
    {
        public List<Article> listArticles { get; set; }
        public int pageIndex { get; set; }
        public int totalCount { get; set; }
    }
}