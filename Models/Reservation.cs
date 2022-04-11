using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wayfare.Models
{
    public class Reservation
    {
        public string Name { get; set; }    
        public int Id { get; set; }
        public string Type { get; set; }    
        public string Adresss { get; set; } 
        public string Confirmation  { get; set; }
        public string CreatorId { get; set; }

    }
}