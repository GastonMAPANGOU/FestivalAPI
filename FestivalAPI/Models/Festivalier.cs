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
        public string Pseudo { get; set; }
        public string Login { get; set; }
        public bool IsPublished { get; set; }
        public string Pwd { get; set; }
        public int NbJours { get; set; }
        public int Nb_ParticipantsPT { get; set; }
        public int Nb_ParticipantsDT { get; set; }
        [ForeignKey("FK_LieuId")]
        public int? LieuId { get; set;}
        public bool InscriptionAccepted { get; set; }
        public double Somme { get; set; }
        [ForeignKey("FK_Festival")]
        public int FestivalId { get;  set; }
        [ForeignKey("FK_Genre")]
        public int GenreId { get; set; }
        public DateTime Date_Inscription { get; set;}
        public DateTime Birthday { get; set; }
        public Festivalier() { }

    }
}
