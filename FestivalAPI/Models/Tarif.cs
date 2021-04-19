using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Models
{
    public class Tarif
    {
        [Key]
        public int IdT { get; set; }
        public double Montant { get; set; }
        public string Type_Tarif { get; set; }
        public int Coefficient { get; set; }
        [ForeignKey("Jour")]
        public int IdJ { get; set; }
        public Tarif() { }
        public Tarif(float montant, int coefficient) 
        {
            Montant = montant;
            Coefficient = coefficient;
            if (Coefficient==2)
            {
                Type_Tarif = "plein tarif";
            }
            else if (Coefficient == 1)
            {
                Type_Tarif = "demi tarif";
            }      
        }

    }
}
