using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Models
{
    public class Favoris
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("FK_Artiste")]
        public int ArtisteId { get; set; }
        [ForeignKey("FK_Festivalier")]
        public int FestivalierId { get; set; }
        public bool Like { get; set; }
    }
}
