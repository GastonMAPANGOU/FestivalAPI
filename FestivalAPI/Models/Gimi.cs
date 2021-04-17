using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Models
{
    public class Gimi
    {
        [Key]
        public int IdG { get; set; }
        public string Nom { get; set; }
        public String Prenom { get; set; }
        public String Login { get; set; }
        public String Pwd { get; set; }
        public Gimi() { }
    }
}
