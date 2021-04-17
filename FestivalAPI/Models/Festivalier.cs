using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Models
{
    public class Festivalier
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Login { get; set; }
        public string Pwd { get; set; }
        public int Nb_Participants { get; set; }
        public double Somme { get; set; }
        [ForeignKey("FK_Festival")]
        public int FestivalId { get;  set; }
        public Festivalier() { }

    }
}
