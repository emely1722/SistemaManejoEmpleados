using SistemaManejoEmpleados.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace SistemaManejoEmpleados.Data
{
    public class AppDbContext : DbContext
    {
       public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) 
           { }


        public DbSet<EmpleadoViewModel> EMPLEADO { get; set; }
    }
}
