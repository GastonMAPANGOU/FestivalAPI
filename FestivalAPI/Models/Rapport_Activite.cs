using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Models
{
    public class Rapport_Activite
    {
        [Key]
        public int Id { get; set; }
        public int Annee { get; set; }
        [ForeignKey("FK_Festival")]
        public int? FestivalId { get; set; }
        public string Departement { get; set; }
        public string Region { get; set; }
        public double Somme_Vente { get; set; }

        public Rapport_Activite() { }
    }
}
