using System;
using System.Collections.Generic;
using System.Text;
using FeastBook_final.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FeastBook_final.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Przepis> Przepisy { get; set; }
        public DbSet<Kategoria> Kategorie { get; set; }
        public DbSet<Produkt> Produkty { get; set; }
        public DbSet<PrzepisProdukt> PrzepisyProdukty {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<PrzepisProdukt>().HasKey(i => new { i.PrzepisId, i.ProduktId });
            builder.Entity<PrzepisProdukt>().HasOne(bc => bc.Przepis).WithMany(b => b.Produkty).HasForeignKey(bc => bc.PrzepisId);
            builder.Entity<PrzepisProdukt>().HasOne(bc => bc.Produkt).WithMany(c => c.Przepisy).HasForeignKey(bc => bc.ProduktId);
        }
    }
}
