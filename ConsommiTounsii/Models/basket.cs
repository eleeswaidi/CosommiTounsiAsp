using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsii.Models
{
    public class basket
    {
        public int id_basket { get; set; }
        public DateTime date_basket { get; set; }
        public float total { get; set; }
        public string type_paiement { get; set; }
    }
}