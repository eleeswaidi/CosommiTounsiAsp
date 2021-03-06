using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsii.Models
{
    public class Product
    {
        public long id_prod { get; set; }

        public String name_prod { get; set; }

        public String description_prod { get; set; }

        public int quantity { get; set; }

        public float price_prod { get; set; }

        public int barcode_prod { get; set; }

        public String weight_prod { get; set; }

        public int minvalue_stock { get; set; }
        
        public List<Claim> claims { get; set; }
        public Line line { get; set; }
        public Category category { get; set; }
        public List<Publicity> publicities  { get; set; }
        public List<Orders> orders { get; set; }

    }
}