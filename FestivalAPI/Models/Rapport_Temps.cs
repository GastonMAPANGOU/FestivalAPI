using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Models
{
    public class Rapport_Temps
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date_Inscription { get; set; }
        [ForeignKey("FK_Festival")]
        public int? FestivalId { get; set; }
        public int Nombre_Inscription { get; set; }

        public Rapport_Temps() { }
    }
}
