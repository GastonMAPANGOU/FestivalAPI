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
        public int AmiDemandeur { get; set; }
        [ForeignKey("FK_Festival")]
        public int AmiReceveur { get; set; }
        public bool Accepted { get; set; }
        public bool Vue { get; set; }

        public Ami() { }
    }
}
