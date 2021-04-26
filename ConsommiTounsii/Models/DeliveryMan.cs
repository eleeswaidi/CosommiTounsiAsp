using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsommiTounsii.Models
{
    public class DeliveryMan:User
    {
        //pour l'ajout et la modification
        public String start_time { get; set; }

        public String end_time { get; set; }


        //pour l'affichage
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }


        public List<Delivery> deliveries { get; set; }
        public List<Premium> premiums { get; set; }
    }
}