using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumingWebAapiRESTinMVC.Models
{
    public class Message
    {
        public long id_message { get; set; }

        public String desc_message { get; set; }

        public Date date_message { get; set; }

    }
}