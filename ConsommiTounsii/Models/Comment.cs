using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumingWebAapiRESTinMVC.Models
{
    public class Comment
    {
        public long id_comment { get; set; }

        public String desc_comment { get; set; }

        public int rating_comment { get; set; }


    }
}