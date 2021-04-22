using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsii.Models
{
    public class Basket
    {
        public int id_basket { get; set; }
        public DateTime date_basket { get; set; }
        public float total { get; set; }
        public Paiement type_paiement { get; set; }
        public Client client { get; set; }
        public List<Orders> orders { get; set; }

    }
}