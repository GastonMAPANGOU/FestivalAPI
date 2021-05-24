using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Models
{
    public class Artiste
    {
        [Key]
        public int IdA { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        public string Photo { get; set; }
        [ForeignKey("FK_Style")]
        public int StyleId { get; set; }
        public string Descriptif { get; set; }
        [ForeignKey("FK_Pays")]
        public int PaysId { get; set; }
        [ForeignKey("FK_Festival")]
        public int? FestivalId { get; set; }
        public ICollection<Festival_Artiste> Festival_Artistes { get; set; }
        public string Extrait { get; set; }
        public Artiste() { }


}
}
