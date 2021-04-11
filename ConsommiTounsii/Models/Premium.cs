using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsii.Models
{
    public class Premium
    {
        public int id{get;set;}
        public float premium{get;set;}
        public String month{get;set;}
        public int year{get;set;}
        public DeliveryMan deliveryMan { get; set; }
    }
}