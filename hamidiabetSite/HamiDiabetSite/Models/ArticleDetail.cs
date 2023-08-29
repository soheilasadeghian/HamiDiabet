using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace HamiDiabet.Models
{
    public class ArticleDetail
    {
        public bool type { get; set; }
        public string text { get; set; }
        public string fileType { get; set; }
        public Binary value { get; set; }
        public int pri { get; set; }
        public DateTime registerDate { get; set; }
    }
}