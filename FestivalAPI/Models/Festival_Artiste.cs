using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;


namespace FestivalAPI.Models
{
    public class Festival_Artiste
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Festival")]
        public int IdF { get; set; }
        [ForeignKey("Artiste")]
        public int IdA { get; set; }
        [ForeignKey("Scene")]
        public int IdS { get; set; }
        [ForeignKey("Jour")]
        public int IdJ { get; set; }
        public Festival_Artiste() { }
    }
}
