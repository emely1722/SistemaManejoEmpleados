using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Runtime.InteropServices;

namespace SistemaManejoEmpleados.Models
{
    [Table("EMPLEADO")]
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
        [Column("GENERO")]
        [Display(Name = "Género")]
        public string GENERO { get; set; }

        [Phone(ErrorMessage = "Número telefónico obligatorio.")]
        [Column("TELEFONO_EMPLEADO")]
        [Display(Name = "Teléfono")]
        public string TELEFONO_EMPLEADO { get; set; }


        [Required(ErrorMessage = "Fecha de inicio obligatoria.")]
        [DataType(DataType.Date)]
        [Column("FECHA_INICIO")]
        [Display(Name = "Fecha de Inicio")]
        public DateTime FECHA_INICIO { get; set; }


        [Required(ErrorMessage = "El salario es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El salario debe ser positivo.")]
        [Column("SALARIO_EMPLEADO")]
        [Display(Name = "Salario")]
        public decimal SALARIO_EMPLEADO { get; set; }

        [Column("ESTADO")]
        [Display(Name = "Estado (Activo/Inactivo)")]
        public bool ESTADO { get; set; }

        [Required(ErrorMessage = "Seleccione un departamento.")]
        [ForeignKey("DEPARTAMENTO")]
        [Display(Name = "Departamento")]
        public int ID_DEPARTAMENTO { get; set; }

        public IEnumerable<SelectListItem> Departamentos { get; set; }

        [Required(ErrorMessage = "Seleccione un cargo.")]
        [ForeignKey("CARGO")]
        [Display(Name = "Cargo")]
        public int ID_CARGO { get; set; }

        public IEnumerable<SelectListItem> Cargos { get; set; }

        [Display(Name = "Tiempo en la Empresa")]
        public string TIEMPO_EN_EMPRESA
        {
            get
            {
                var hoy = DateTime.Today;
                var inicio = FECHA_INICIO;
                var tiempo = hoy - inicio;
                var años = (int)(tiempo.TotalDays / 365.25);
                var meses = (int)((tiempo.TotalDays % 365.25) / 30);
                return $"{años} años y {meses} meses";
            }
        }

        public virtual CargoViewModel IdCargoNavigation { get; set; }
        public virtual DepartamentoViewModel IdDepartamentoNavigation { get; set; }

		public string NOMBRE_CARGO { get; set; }
		public string NOMBRE_DEPARTAMENTO { get; set; }




	}
}
