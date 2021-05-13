using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FestivalAPI.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; }
        
        public ICollection<Festivalier> Festivaliers { get; set; }

        public Genre() { }

    }
}
