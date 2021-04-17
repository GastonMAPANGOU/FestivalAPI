using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Models
{
    public class Festival
    {
        [Key]
        public int IdF { get; set; }
        public string Nom { get; set; }
        public string Logo { get; set; }
        public string Descriptif { get; set; }
        public DateTime Date_Debut { get; set; }
        public DateTime Date_Fin { get; set; }
        [ForeignKey("FK_Lieu")]
        public int LieuId { get; set; }
        public Organisateur Organisateur { get; set; }
        public ICollection<Festivalier> Festivaliers { get; set; }
        public Festival() { }
    }
}
