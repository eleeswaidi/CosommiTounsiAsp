using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsii.Models
{
    public class orders
    {
        public int id_orders { get; set; }
        public string status_order { get; set; }
        public DateTime date_order { get; set; }
        public float fees_order { get; set; }
        public int quantity { get; set; }
    }
}