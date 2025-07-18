using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaManejoEmpleados.Models;

public partial class DepartamentoViewModel
{
    [Key]
    public int ID_DEPARTAMENTO { get; set; }

    [Required]
    [StringLength(100)]
    public string NOMBRE_DEPARTAMENTO { get; set; } = null!;

    public virtual ICollection<EmpleadoViewModel> Empleados { get; set; } = new List<EmpleadoViewModel>();
}
