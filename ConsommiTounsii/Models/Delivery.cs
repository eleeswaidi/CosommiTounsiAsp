using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsii.Models
{
    public class Delivery
    {

        public int id{get;set;}
        public DateTime date{get;set;}
        public String status{get;set;}
        public float fees{get;set;}
    }
}