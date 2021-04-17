using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Models
{
    public class Hebergement
    {
        [Key]
        public int IdH { get; set; }
        public string Lien { get; set; }
        [ForeignKey("FK_Lieu")]
        public int LieuId { get; set; }
        [ForeignKey("FK_Type_Hebergement")]
        public int Type_HebergementId { get; set; }
        public Hebergement() { }


    }
}
