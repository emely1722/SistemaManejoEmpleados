using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaManejoEmpleados.Models
{
    public class EmpleadoViewModel
    {
        [Key]
        public int ID_EMPLEADO { get; set; }

        [Required (ErrorMessage = "Nombre es OBLIGATORIO.")]
        [StringLength (100)]
        [Column ("NOMBRE_EMPLEADO")]
        [Display(Name ="Nombre del Empleado.")]
        public string NOMBRE_EMPLEADO { get; set;}

        [Required(ErrorMessage = "La cédula es obligatoria.")]
        [StringLength(20)]
        [Column("CEDULA_EMPLEADO")]
        [Display(Name = "Cédula")]
        public string CEDULA_EMPLEADO { get; set; }
        [Required(ErrorMessage = "El género es obligatorio.")]
        [Display(Name = "Género")]
        public string GENERO { get; set; }

        [Phone(ErrorMessage = "Número telefónico inválido.")]
        [Display(Name = "Teléfono")]
        public string TELEFONO_EMPLEADO { get; set; }

        [Required(ErrorMessage = "Fecha de inicio obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Inicio")]
        public DateOnly FECHA_INICIO { get; set; }

        [Required(ErrorMessage = "El salario es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El salario debe ser positivo.")]
        [Display(Name = "Salario")]
        public decimal SALARIO_EMPLEADO { get; set; }

        [Display(Name = "Estado (Activo/Inactivo)")]
        public bool ESTADO { get; set; }

        [Required(ErrorMessage = "Seleccione un departamento.")]
        [Display(Name = "Departamento")]
        public int ID_DEPARTAMENTO { get; set; }

        [Display(Name = "Nombre del Departamento")]
        public string NOMBRE_DEPARTAMENTO { get; set; }

        [Required(ErrorMessage = "Seleccione un cargo.")]
        [Display(Name = "Cargo")]
        public int ID_CARGO { get; set; }

        [Display(Name = "Nombre del Cargo")]
        public string NOMBRE_CARGO { get; set; }

        [Display(Name = "Tiempo en la Empresa")]
        public string TIEMPO_EN_EMPRESA
        {
            get
            {
                var hoy = DateTime.Today;
                var inicio = FECHA_INICIO.ToDateTime(TimeOnly.MinValue);
                var tiempo = hoy - inicio;
                var años = (int)(tiempo.TotalDays / 365.25);
                var meses = (int)((tiempo.TotalDays % 365.25) / 30);
                return $"{años} años y {meses} meses";
            }
        }
    }
}
