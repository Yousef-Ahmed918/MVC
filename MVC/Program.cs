using DataAcess.Contexts;
using Microsoft.EntityFrameworkCore;
using DataAccess.Repositories.Interfaces;
using DataAccess.Repositories.Classes;
using BussinessLogic.Services.Classes;
using BussinessLogic.Services.Interfaces;
using BussinessLogic.Profiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }); 
            //Department
            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();
            //Employee
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService , EmployeeService>();
            //Auto Mapper
            builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            builder.Services.AddAutoMapper(p => p.AddProfile(new MappingProfiles()));

            //Options
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                //options.UseSqlServer(builder.Configuration["ConnctionStrings:DefaultConnection"]);//Reads from appsitting jason file
                //OR
                //options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
                //OR
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                //Lazy Loading
                options.UseLazyLoadingProxies();


            }); //Register Service in DI Container


            #endregion

            var app = builder.Build();
            #region Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            #endregion

            app.Run();
        }
    }
}
