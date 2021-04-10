using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsii.Models
{
    public class Message
    {
        public long id_message { get; set; }

        public String desc_message { get; set; }

        public DateTime date_message { get; set; }

    }
}