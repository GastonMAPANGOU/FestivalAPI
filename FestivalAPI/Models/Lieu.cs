using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace FestivalAPI.Models
{
    public class Lieu
    {
        [Key]
        public int IdL { get; set; }
        public string Commune { get; set; }
        [ForeignKey("FK_Departement")]
        public int DepartementId { get; set; }
        public ICollection<Festival> Festivals { get; set; }
        public ICollection<Festivalier> Festivaliers { get; set; }
        public ICollection<Scene> Scenes { get; set; }
        public Lieu() { }
    }
}
