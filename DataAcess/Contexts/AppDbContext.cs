using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.DepartmentModels;
using DataAccess.Models.EmployeeModels;

namespace DataAcess.Contexts
{
    public class AppDbContext:DbContext
    {

        //Dependency Injection 
        //public AppDbContext(Nada nada) //Ask the CLR to create object from Nada
        //{
        //step 01 : Inject 
        //Step 02 : Add services to container in the main
        //}
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlServer("ConnectionString");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //OR
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
           
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
