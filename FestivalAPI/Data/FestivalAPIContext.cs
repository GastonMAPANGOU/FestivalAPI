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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lieu>()
                .HasIndex(l => new { l.Commune})
                .IsUnique(true);
            modelBuilder.Entity<Gimi>()
                .HasIndex(g => new { g.Login,g.Pwd })
                .IsUnique(true);
            modelBuilder.Entity<Organisateur>()
                .HasIndex(g => new { g.Login, g.Pwd })
                .IsUnique(true);
            modelBuilder.Entity<Festivalier>()
                .HasIndex(g => new { g.Login, g.Pwd,g.FestivalId })
                .IsUnique(true);
            modelBuilder.Entity<Tarif>()
                .HasIndex(l => new { l.Type_Tarif })
                .IsUnique(true);
            modelBuilder.Entity<Pays>()
                .HasIndex(l => new { l.Nom })
                .IsUnique(true);
            modelBuilder.Entity<Type_Hebergement>()
                .HasIndex(l => new { l.Type })
                .IsUnique(true);
            modelBuilder.Entity<Scene>()
                .HasIndex(l => new { l.Nom,l.Adresse })
                .IsUnique(true);
            modelBuilder.Entity<Ami>()
                .HasIndex(l => new { l.AmiDemandeur,l.AmiReceveur})
                .IsUnique(true);
            modelBuilder.Entity<Artiste>()
                .HasIndex(l => new { l.Nom,l.Prenom })
                .IsUnique(true);
            modelBuilder.Entity<Style>()
                .HasIndex(l => new { l.Nom })
                .IsUnique(true);
            modelBuilder.Entity<Festival>()
                .HasIndex(g => new { g.Nom })
                .IsUnique(true);
            modelBuilder.Entity<Hebergement>()
                .HasIndex(g => new { g.Lien })
                .IsUnique(true);
            modelBuilder.Entity<Festival_Artiste>()
                .HasIndex(g => new { g.FestivalId, g.JourId,g.SceneId,g.ArtisteId})
                .IsUnique(true);
        }
    }
}
