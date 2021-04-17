using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Models
{
    public class Type_Hebergement
    {
        [Key]
        public int IDTH { get; set; }
        public string Type { get; set; }
        public ICollection<Hebergement> Hebergements { get; set; }
        public Type_Hebergement() { }
    }
}
