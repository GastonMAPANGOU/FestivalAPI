using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Models
{
    public class Jour
    {
        [Key]
        public int IdJ { get; set; }
        public string Numero_jour { get; set; }
        public DateTime Date_jour { get; set;}
        [ForeignKey("FK_Festival")]
        public int? FestivalId { get; set; }
        public ICollection<Tarif> Tarifs { get; set; }
        public ICollection<Festival_Artiste> Festival_Artistes { get; set; }
        public Jour() { }
    }
}
