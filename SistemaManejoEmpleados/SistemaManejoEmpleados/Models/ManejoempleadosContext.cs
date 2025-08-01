﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SistemaManejoEmpleados.Models;

public partial class ManejoempleadosContext : DbContext
{
    public ManejoempleadosContext()
    {
    }

    public ManejoempleadosContext(DbContextOptions<ManejoempleadosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-0HDEU2U1;Database=MANEJOEMPLEADOS;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.IdCargo).HasName("PK__CARGO__6F4DBE2BE8BB4648");

            entity.ToTable("CARGO");

            entity.Property(e => e.IdCargo).HasColumnName("ID_CARGO");
            entity.Property(e => e.NombreCargo)
                .HasMaxLength(100)
                .HasColumnName("NOMBRE_CARGO");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PK__DEPARTAM__52E77BE041C643A6");

            entity.ToTable("DEPARTAMENTO");

            entity.Property(e => e.IdDepartamento).HasColumnName("ID_DEPARTAMENTO");
            entity.Property(e => e.NombreDepartamento)
                .HasMaxLength(100)
                .HasColumnName("NOMBRE_DEPARTAMENTO");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__EMPLEADO__922CA2692EDA7F80");

            entity.ToTable("EMPLEADO");

            entity.Property(e => e.IdEmpleado).HasColumnName("ID_EMPLEADO");
            entity.Property(e => e.CedulaEmpleado)
                .HasMaxLength(100)
                .HasColumnName("CEDULA_EMPLEADO");
            entity.Property(e => e.Estado).HasColumnName("ESTADO");
            entity.Property(e => e.FechaInicio).HasColumnName("FECHA_INICIO");
            entity.Property(e => e.Genero)
                .HasMaxLength(10)
                .HasColumnName("GENERO");
            entity.Property(e => e.IdCargo).HasColumnName("ID_CARGO");
            entity.Property(e => e.IdDepartamento).HasColumnName("ID_DEPARTAMENTO");
            entity.Property(e => e.NombreEmpleado)
                .HasMaxLength(100)
                .HasColumnName("NOMBRE_EMPLEADO");
            entity.Property(e => e.SalarioEmpleado)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("SALARIO_EMPLEADO");
            entity.Property(e => e.TelefonoEmpleado)
                .HasMaxLength(20)
                .HasColumnName("TELEFONO_EMPLEADO");

            entity.HasOne(d => d.IdCargoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdCargo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EMPLEADO__ID_CAR__4222D4EF");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EMPLEADO__ID_DEP__412EB0B6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
