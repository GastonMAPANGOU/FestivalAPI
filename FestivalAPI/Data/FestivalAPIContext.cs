using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FestivalAPI.Models;


namespace FestivalAPI.Data
{
    public class FestivalAPIContext : DbContext
    {
        public FestivalAPIContext(DbContextOptions<FestivalAPIContext> options)
            : base(options)
        {
        }

        public DbSet<FestivalAPI.Models.Festival> Festival { get; set; }

        public DbSet<FestivalAPI.Models.Hebergement> Hebergement { get; set; }

        public DbSet<FestivalAPI.Models.Organisateur> Organisateur { get; set; }

        public DbSet<FestivalAPI.Models.Scene> Scene { get; set; }

        public DbSet<FestivalAPI.Models.Type_Hebergement> Type_Hebergement { get; set; }

        public DbSet<FestivalAPI.Models.Artiste> Artiste { get; set; }

        public DbSet<FestivalAPI.Models.Festival_Artiste> Festival_Artiste { get; set; }

        public DbSet<FestivalAPI.Models.Gimi> Gimi { get; set; }

        public DbSet<FestivalAPI.Models.Jour> Jour { get; set; }

        public DbSet<FestivalAPI.Models.Lieu> Lieu { get; set; }

        public DbSet<FestivalAPI.Models.Tarif> Tarif { get; set; }

        public DbSet<FestivalAPI.Models.Festivalier> Festivalier { get; set; }

        public DbSet<FestivalAPI.Models.Pays> Pays { get; set; }

        public DbSet<FestivalAPI.Models.Style> Style { get; set; }

        public DbSet<FestivalAPI.Models.Ami> Ami { get; set; }
    }
}
