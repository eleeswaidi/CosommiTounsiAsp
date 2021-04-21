using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsii.Models
{
    public class Client :User
    {

        public int status_client { get; set; }
        public List<Subject> subjects { get; set; }
        public List<Message> messages { get; set; }
        public Basket basket { get; set; }


    }
}