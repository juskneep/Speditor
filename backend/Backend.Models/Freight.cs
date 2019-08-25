using Backend.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Backend.Models
{
    public class Freight
    {
        [Key]
        public string Id { get; set; }  
        public string LoadingPlace { get; set; }
        public string DumpingPlace { get; set; }
        public string PostedFrom { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime LoadingDate { get; set; }
        public DateTime OfferDate { get; set; }
        public FreightType FreightType { get; set; }
        public int GrossWeight { get; set; }
        public int Price { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
