using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsii.Models
{
    public class Subject
    {
        public long id_subject { get; set; }

        public String desc_subject { get; set; }

        public int rating_subject { get; set; }
        public List<Comment> comments { get; set; } 
        public Product product { get; set; }
        
        public Client client { get; set; }
    }
}