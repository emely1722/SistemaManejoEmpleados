using System;
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

    public virtual DbSet<CargoViewModel> Cargos { get; set; }

    public virtual DbSet<DepartamentoViewModel> Departamentos { get; set; }

    public virtual DbSet<EmpleadoViewModel> Empleados { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-0HDEU2U1;Database=MANEJOEMPLEADOS;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CargoViewModel>(entity =>
        {
            entity.HasKey(e => e.ID_CARGO).HasName("PK__CARGO__6F4DBE2BE8BB4648");

            entity.ToTable("CARGO");

            entity.Property(e => e.ID_CARGO).HasColumnName("ID_CARGO");
            entity.Property(e => e.NOMBRE_CARGO)
                .HasMaxLength(100)
                .HasColumnName("NOMBRE_CARGO");
        });

        modelBuilder.Entity<DepartamentoViewModel>(entity =>
        {
            entity.HasKey(e => e.ID_DEPARTAMENTO).HasName("PK__DEPARTAM__52E77BE041C643A6");

            entity.ToTable("DEPARTAMENTO");

            entity.Property(e => e.ID_DEPARTAMENTO).HasColumnName("ID_DEPARTAMENTO");
            entity.Property(e => e.NOMBRE_DEPARTAMENTO)
                .HasMaxLength(100)
                .HasColumnName("NOMBRE_DEPARTAMENTO");
        });

        modelBuilder.Entity<EmpleadoViewModel>(entity =>
        {
            entity.HasKey(e => e.ID_EMPLEADO).HasName("PK__EMPLEADO__922CA269D51A1741");

            entity.ToTable("EMPLEADO");

            entity.Property(e => e.ID_EMPLEADO).HasColumnName("ID_EMPLEADO");
            entity.Property(e => e.CEDULA_EMPLEADO)
                .HasMaxLength(100)
                .HasColumnName("CEDULA_EMPLEADO");
            entity.Property(e => e.ESTADO).HasColumnName("ESTADO");
            entity.Property(e => e.FECHA_INICIO).HasColumnName("FECHA_INICIO");
            entity.Property(e => e.GENERO)
                .HasMaxLength(10)
                .HasColumnName("GENERO");
            entity.Property(e => e.NOMBRE_CARGO).HasColumnName("NOMBRE_CARGO");
            entity.Property(e => e.NOMBRE_DEPARTAMENTO).HasColumnName("NOMBRE_DEPARTAMENTO");
            entity.Property(e => e.NOMBRE_EMPLEADO)
                .HasMaxLength(20)
                .HasColumnName("NOMBRE_EMPLEADO");
            entity.Property(e => e.SALARIO_EMPLEADO)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("SALARIO_EMPLEADO");
            entity.Property(e => e.TELEFONO_EMPLEADO)
                .HasMaxLength(20)
                .HasColumnName("TELEFONO_EMPLEADO");

            entity.HasOne(d => d.IdCargoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.NOMBRE_CARGO)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EMPLEADO__ID_CAR__3E52440B");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.NOMBRE_DEPARTAMENTO)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EMPLEADO__ID_DEP__3D5E1FD2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
