using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.DepartmentModels;
using DataAccess.Models.EmployeeModels;
using DataAccess.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DataAcess.Contexts
{
    //Use IdentityDbContext to include the dbset automatinc instead of the DbContext
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options) 
    {

        //Dependency Injection 
        //public AppDbContext(Nada nada) //Ask the CLR to create object from Nada
        //{
        //step 01 : Inject 
        //Step 02 : Add services to container in the main
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    ////Lazy Loading
        //    //optionsBuilder.UseLazyLoadingProxies();
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
            //OR
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
           
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

    
    }
}
