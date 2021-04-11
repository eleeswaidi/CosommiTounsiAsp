using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsii.Models
{
    public class Publicity
    {
        public long id_pub { get; set; }

        public String name_pub { get; set; }

        public Product product { get; set; }
    }
}