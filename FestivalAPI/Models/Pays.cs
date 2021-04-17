using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Models
{
    public class Pays
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; }

        public ICollection<Artiste> Artistes { get; set; }
        public Pays() { }
    }
}
