﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace FestivalAPI.Models
{
    public class Scene
    {
        [Key]
        public int IdS { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public int Capacite { get; set; } 
        public int Accessibilite { get; set; }
        [ForeignKey("FK_Lieu")]
        public int LieuId { get; set; }
        public Scene() { }
    }
}
