using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGestionStock.Model
{
    public class ArticleCategorie
    {
        public int ArticleId { get; set; }
        public string ArticleDesignation { get; set; }
        public double? ArticlePrix { get; set; }
        public int? ArticleQuantiteMin { get; set; }
        public int CategorieId { get; set; }
        public string CategorieNom { get; set; }
        public string CategorieInfo { get; set; }
    }
}
