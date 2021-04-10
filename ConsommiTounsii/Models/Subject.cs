using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumingWebAapiRESTinMVC.Models
{
    public class Subject
    {
        public long id_subject { get; set; }

        public String desc_subject { get; set; }

        public int rating_subject { get; set; }
    }
}