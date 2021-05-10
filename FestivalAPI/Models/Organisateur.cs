using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Models
{
    public class Organisateur
    {
        [Key]
        public int IdO { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Login { get; set; }
        public string Pwd { get; set; }
        [ForeignKey("FK_Festival")]
        public int FestivalId { get; set; }
        public Organisateur() { }

    }
}
