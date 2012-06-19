using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using wepp_app_v0.Models;


namespace wepp_app_v0.Context
{
    public class EFDbContext:DbContext
    {
    
        public DbSet<Requerimiento> Requerimientos { get; set; }
        public DbSet<PersonalInterno> PersonalesInternos { get; set; }
        public DbSet<Cotizacion> Cotizaciones { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //Tabla Requerimiento
            modelBuilder.Entity<Requerimiento>().ToTable("Requerimiento");
            //modelBuilder.Entity<Requerimiento>().HasKey(t => t.IdRequerimiento);

            modelBuilder.Entity<Requerimiento>()
                .HasOptional(s => s.LiderProyecto)
                .WithMany()
                .HasForeignKey(s => s.IdLiderProyecto)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Requerimiento>()
                .HasOptional(s => s.IdS)
                .WithMany(l => l.Requerimientos)
                .HasForeignKey(s => s.IdIdS)
                .WillCascadeOnDelete(false);
            

            //Tabla PersonalInterno
            modelBuilder.Entity<PersonalInterno>().ToTable("PersonalInterno");


            //Tabla Cotizacion
            modelBuilder.Entity<Cotizacion>().ToTable("Cotizacion");
            modelBuilder.Entity<Cotizacion>()
                .HasRequired(p => p.requerimiento)
                .WithMany(l => l.cotizaciones)
                .HasForeignKey(p => p.IdRequerimiento)
                .WillCascadeOnDelete(false);

            

        }
    }
}