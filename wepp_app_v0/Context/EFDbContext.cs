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
        public DbSet<Cronograma> Cronogramas { get; set; }
        public DbSet<Actividad> Actividades { get; set; }
        public DbSet<Vacacion> Vacaciones { get; set; }


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

            //Tabla Vacaciones
            modelBuilder.Entity<Vacacion>().ToTable("Vacaciones");

            modelBuilder.Entity<Vacacion>()
                .HasRequired(s => s.personalInterno)
                .WithMany(l=>l.Vacaciones)
                .HasForeignKey(s => s.IdPersonalInterno)
                .WillCascadeOnDelete(false);


            //Tabla PersonalInterno
            modelBuilder.Entity<PersonalInterno>().ToTable("PersonalInterno");


            //Tabla Cotizacion
            modelBuilder.Entity<Cotizacion>().ToTable("Cotizacion");
            modelBuilder.Entity<Cotizacion>()
                .HasRequired(p => p.requerimiento)
                .WithMany(l => l.Cotizaciones)
                .HasForeignKey(p => p.IdRequerimiento)
                .WillCascadeOnDelete(false);

            //Tabla Cronograma
            modelBuilder.Entity<Cronograma>().ToTable("Cronograma");
            modelBuilder.Entity<Cronograma>()
                .HasRequired(p => p.requerimiento)
                .WithMany(l => l.Cronogramas)
                .HasForeignKey(p => p.IdRequerimiento)
                .WillCascadeOnDelete(false);

            //Tabla Actividad
            modelBuilder.Entity<Actividad>().ToTable("Actividad");

            modelBuilder.Entity<Actividad>()
                .HasRequired(p => p.cronograma)
                .WithMany(l => l.Actividades)
                .HasForeignKey(p => p.IdCronograma)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Actividad>()
                .HasOptional(p => p.personalInterno)
                .WithMany(l => l.Actividades)
                .HasForeignKey(p => p.IdPersonalInterno)
                .WillCascadeOnDelete(false);

            

        }

        
    }
}