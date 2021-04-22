using System;

namespace ConsommiTounsii.Models
{
    public class Orders
    {
        public long id_order { get; set; }
        public string status_order { get; set; }
        public DateTime date_order { get; set; }
        public float fees_order { get; set; }
        public int quantity { get; set; }
      
        public Delivery delivery { get; set; }
        public Basket basket { get; set; }

        public Product product { get; set; }
    }
}