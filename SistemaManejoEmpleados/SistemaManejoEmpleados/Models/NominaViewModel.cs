using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SistemaManejoEmpleados.Models
{
    public class NominaViewModel
    {
        public int ID_EMPLEADO { get; set; }
        public string NOMBRE_EMPLEADO { get; set; }
        public decimal SALARIO_EMPLEADO { get; set; }

        public decimal AFP => decimal.Round(SALARIO_EMPLEADO * 0.0287M, 2);
        public decimal ARS => decimal.Round(SALARIO_EMPLEADO * 0.0304M, 2);
        public decimal ISR => decimal.Round(CalcularISR(SALARIO_EMPLEADO), 2);
        public decimal SALARIO_NETO => decimal.Round(SALARIO_EMPLEADO - AFP - ARS - ISR, 2);
        public decimal TOTAL_DESCUENTO => AFP + ARS + ISR;
        public DateTime FECHA_INGRESO { get; set; } 
        public string TiempoEnEmpresa
        {
            get
            {
                var hoy = DateTime.Today;
                var años = hoy.Year - FECHA_INGRESO.Year;
                var meses = hoy.Month - FECHA_INGRESO.Month;    

                if (meses < 0)
                {
                    años--;
                    meses += 12;
                }

                return $"{años} año(s), {meses} mes(es)";
            }
        }

        private decimal CalcularISR(decimal salarioMensual)
        {
            var salarioAnual = salarioMensual * 12;
            if (salarioAnual <= 416220) return 0;
            if (salarioAnual <= 624329) return ((salarioAnual - 416220) * 0.15M) / 12;
            if (salarioAnual <= 867123) return (((salarioAnual - 624329) * 0.20M) + 31216) / 12;
            return (((salarioAnual - 867123) * 0.25M) + 79776) / 12;
        }

    }
}
