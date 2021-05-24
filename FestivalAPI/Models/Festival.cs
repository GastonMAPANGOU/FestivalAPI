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
        public bool IsFree { get; set; }
        public bool IsCanceled { get; set; }
        //public bool InscriptionsArePossible { get; set; }
        public int NbPlacesDispo { get; set; }
        public double Montant { get; set; }
        public DateTime Date_Debut { get; set; }
        public DateTime Date_Fin { get; set; }
        [ForeignKey("FK_Lieu")]
        public int LieuId { get; set; }
        public ICollection<Organisateur> Organisateurs { get; set; }
        public ICollection<Festivalier> Festivaliers { get; set; }
        public ICollection<Hebergement> Hebergements { get; set; }
        public ICollection<Festival_Artiste> Festival_Artistes { get; set; }
        public ICollection<Artiste> Artistes { get; set; }
        public ICollection<Jour> Jours { get; set; }
        public ICollection<Scene> Scenes { get; set; }
        public Festival() { }
    }
}
