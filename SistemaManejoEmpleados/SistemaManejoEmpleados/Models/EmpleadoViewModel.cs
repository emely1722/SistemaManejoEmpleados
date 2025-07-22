using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaManejoEmpleados.Models;

public partial class EmpleadoViewModel
{
    [Key]
    public int ID_EMPLEADO { get; set; }

    [Required (ErrorMessage = "Nombre es obligatorio.")]
    [StringLength (20)]
    [Column ("NOMBRE_EMPLEADO")]
    [Display (Name = "Nombre completo del empleado.")]
    public string NOMBRE_EMPLEADO { get; set; }

    [Required (ErrorMessage = "Cédula es obligatoria.")]
    [StringLength (16), MinLength(6)]
    [Column("CEDULA_EMPLEADO")]
    [Display (Name = "Cédula.")]
    public string CEDULA_EMPLEADO { get; set; }

    [Required(ErrorMessage = "Género obligatorio")]
    [Column("GENERO")]
    [Display (Name = "Género")]
    public string? GENERO { get; set; }

    [Phone (ErrorMessage = "El número telefónico es obligatorio")]
    [MinLength(10)]
    [Column("TELEFONO_EMPLEADO")]
    [Display(Name = "Número")]
    public string? TELEFONO_EMPLEADO { get; set; }

    [Required(ErrorMessage = "Escriba fecha de inicio")]
    [DataType(DataType.Date)]
    [Column("FECHA_INICIO")]
    [Display(Name = "Fecha de Inicio")]
    public DateOnly FECHA_INICIO { get; set; }

    [NotMapped]
    [Required(ErrorMessage = "El salario es obligatorio.")]
    [Range(0, double.MaxValue, ErrorMessage = "El salario debe ser un valor positivo.")]
    public string TIEMPOENEMPRESA
    {
        get
        {
            var hoy = DateTime.Today;
            var fechaInicio = FECHA_INICIO.ToDateTime(TimeOnly.MinValue); // convierte DateOnly a DateTime
            var tiempo = hoy - fechaInicio;
            var años = (int)(tiempo.Days / 365.25);
            var meses = (int)((tiempo.Days % 365.25) / 30);
            return $"{años} años y {meses} meses";
        }
    }
    public decimal SALARIO_EMPLEADO { get; set; }

    [Required]
    [Column("ESTADO")]
    [Display(Name = "Estado del empleado")]
    public bool ESTADO { get; set; } //Activo o Inactivo


    //FK
    //public int ID_DEPARTAMENTO { get; set; }
    //public int ID_CARGO { get; set; }

    [Required(ErrorMessage = "Indique el departamento")]
    [Column("NOMBRE_DEPARTAMENTO")]
    public string NOMBRE_DEPARTAMENTO { get; set; }

    [Required (ErrorMessage = "Indique el cargo del empleado")]
    [Column ("NOMBRE_CARGO")]
    public string NOMBRE_CARGO { get; set; }

    public virtual CargoViewModel IdCargoNavigation { get; set; } = null!;

    public virtual DepartamentoViewModel IdDepartamentoNavigation { get; set; } = null!;
}
