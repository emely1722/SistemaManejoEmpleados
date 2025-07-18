using System;
using System.Collections.Generic;

namespace SistemaManejoEmpleados.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string NombreEmpleado { get; set; } = null!;

    public string CedulaEmpleado { get; set; } = null!;

    public string? Genero { get; set; }

    public string? TelefonoEmpleado { get; set; }

    public DateOnly FechaInicio { get; set; }

    public decimal SalarioEmpleado { get; set; }

    public bool Estado { get; set; }

    public int IdDepartamento { get; set; }

    public int IdCargo { get; set; }

    public virtual Cargo IdCargoNavigation { get; set; } = null!;

    public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;
}
