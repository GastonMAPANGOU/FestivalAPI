using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Models
{
    public class Rapport_Geo
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("FK_Festival")]
        public int? FestivalId { get; set; }
        [ForeignKey("FK_Festivalier")]
        public int? FestivalierId { get; set; }
        public string Pays { get; set; }
        public string Departement { get; set; }
        public string Region { get; set; }
        public string Genre { get; set; }

        public Rapport_Geo() { }
    }
}
