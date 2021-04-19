using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;


namespace FestivalAPI.Models
{
    public class Ami
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("FK_Festival")]
        public int Ami1 { get; set; }
        [ForeignKey("FK_Festival")]
        public int Ami2 { get; set; }
        public bool Accepted { get; set; }

        public Ami() { }
    }
}
