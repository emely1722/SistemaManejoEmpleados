using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SistemaManejoEmpleados.Models
{
    public class DepartamentoViewModel
    {
        [Key]
        public int ID_DEPARTAMENTO {  get; set; }

        [Required (ErrorMessage = "Nombre del departamento obligatorio")]
        [StringLength (50)]
        [Column ("NOMBRE_DEPARTAMENTO")]
        [Display(Name ="Nombre del Departamento")]
        public string NOMBRE_DEPARTAMENTO { get; set; }
    }
}
