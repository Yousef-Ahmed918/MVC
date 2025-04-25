using DataAcess.Contexts;
using Microsoft.EntityFrameworkCore;
using DataAccess.Repositories.Interfaces;
using DataAccess.Repositories.Classes;
using BussinessLogic.Services.Classes;
using BussinessLogic.Services.Interfaces;
using BussinessLogic.Profiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using BussinessLogic.Services.AttachmentServices;
using DataAccess.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;

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
            builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();
            //Employee
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            #region Before Unit Of Work
            //builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            #endregion
            //Unit Of Work 
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //Auto Mapper
            builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            builder.Services.AddAutoMapper(p => p.AddProfile(new MappingProfiles()));
            //Attachment Service
            builder.Services.AddScoped<IAttechmentService, AttachmentService>();
            //Register Service 
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders(); //If There more than one dbContext

            //builder.Services.ConfigureApplicationCookie(config =>
            //{
            //    config.LoginPath = "Account/LogIn";
            //});
            //This happens by default from .NET

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

            //Login With Token
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=LogIn}/{id?}");

            #endregion

            app.Run();
        }
    }
}
