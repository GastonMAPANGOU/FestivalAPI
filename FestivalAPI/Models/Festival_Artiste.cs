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
        [ForeignKey("FK_Festival")]
        public int FestivalId { get; set; }
        [ForeignKey("FK_Artiste")]
        public int? ArtisteId { get; set; }
        [ForeignKey("FK_Scene")]
        public int? SceneId { get; set; }
        [ForeignKey("FK_Jour")]
        public int JourId { get; set; }
        public DateTime HeureDebut { get; set; }
        public DateTime HeureFin { get; set; }
        public Festival_Artiste() { }
    }
}
