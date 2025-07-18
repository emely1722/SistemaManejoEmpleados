using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaManejoEmpleados.Models;

public partial class Cargo
{
    [Key]
    public int ID_CARGO { get; set; }

    [Required]
    [StringLength(100)]
    public string NOMBRE_CARGO { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
