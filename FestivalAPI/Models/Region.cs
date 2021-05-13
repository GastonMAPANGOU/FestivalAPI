using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FestivalAPI.Models
{
    public class Region
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; }
        [ForeignKey("FK_Pays")]
        public int? PaysId { get; set; }
        public ICollection<Departement> Departements { get; set; }

        public Region() { }

    }
}
