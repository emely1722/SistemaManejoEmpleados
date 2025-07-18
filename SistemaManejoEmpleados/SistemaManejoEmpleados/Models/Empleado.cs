using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaManejoEmpleados.Models;

public partial class Empleado
{
    [Key]
    public int ID_EMPLEADO { get; set; }

    [Required (ErrorMessage = "Nombre es obligatorio.")]
    [StringLength (20)]
    [Display (Name = "Nombre completo del empleado.")]
    public string NOMBRE_EMPLEADO { get; set; } = null!;

    [Required (ErrorMessage = "Cédula es obligatoria.")]
    [StringLength (16), MinLength(6)]
    [Display (Name = "Cédula.")]
    public string CEDULA_EMPLEADO { get; set; } = null!;

    [Required]
    [Display (Name = "Género")]
    public string? GENERO { get; set; }

    [Phone]
    [MinLength(10)]
    [Display(Name = "Número")]
    public string? TELEFONO_EMPLEADO { get; set; }

    [Required(ErrorMessage = "Escriba fecha de inicio")]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha de Inicio")]
    public DateOnly FECHA_INICIO { get; set; }


    [Required(ErrorMessage = "El salario es obligatorio.")]
    [Range(0, double.MaxValue, ErrorMessage = "El salario debe ser un valor positivo.")]
    [Column(TypeName = "decimal(10,2)")]

    public decimal SALARIO_EMPLEADO { get; set; }

    [Required]
    [Display(Name = "Estado del empleado")]
    public bool ESTADO { get; set; } //Activo o Inactivo


    //FK
    public int IdDepartamento { get; set; }
    public int IdCargo { get; set; }

    public virtual Cargo IdCargoNavigation { get; set; } = null!;

    public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;
}
