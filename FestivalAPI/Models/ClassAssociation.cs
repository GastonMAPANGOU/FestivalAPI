using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Models
{
    public class ClassAssociation
    {
        public int IdF{ get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public Festival festival { get; set; }
        public int IdA { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public Artiste artiste { get; set; }
    }
}
