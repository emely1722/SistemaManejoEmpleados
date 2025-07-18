using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaManejoEmpleados.Models;

public partial class CargoViewModel
{
    [Key]
    public int ID_CARGO { get; set; }

    [Required]
    [StringLength(100)]
    public string NOMBRE_CARGO { get; set; } = null!;

    public virtual ICollection<EmpleadoViewModel> Empleados { get; set; } = new List<EmpleadoViewModel>();
}
