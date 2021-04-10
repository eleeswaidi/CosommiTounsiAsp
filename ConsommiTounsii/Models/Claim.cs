using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsommiTounsii.Models
{
    public class Claim
    {
        private Product product;

        private User user;

        private long id_claim;
        public String description{get;set;}
        public Status_Claim status{get;set;}


    }


    public enum Status_Claim
    {
        TREATED, UNTREATED, REFUSED
    }
}