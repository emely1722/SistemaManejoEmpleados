using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaManejoEmpleados.Models
{
    public class CargoViewModel
    {
        [Key]
        public int ID_CARGO {  get; set; }

        [Required (ErrorMessage ="El Cargo es Obligatorio")]
        [StringLength (50)]
        [Column ("NOMBRE_CARGO")]
        [Display(Name ="Nombre del Cargo")]
        public string NOMBRE_CARGO { get; set; }

        public List<EmpleadoViewModel> Empleado { get; set; } 

    }
}
