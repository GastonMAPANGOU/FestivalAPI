using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Models
{
    public class Departement
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; }
        [ForeignKey("FK_Region")]
        public int RegionId { get; set; }
        public ICollection<Lieu> Lieux { get; set; }
        public Departement() { }
    }
}
