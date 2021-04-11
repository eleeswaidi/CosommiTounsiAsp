using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsii.Models
{
    public class LineManager:User
    {
        public string status { get; set; }
        public Line line { get; set; }
    }
}