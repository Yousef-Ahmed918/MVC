using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess.Contexts;

namespace DataAccess.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork ,IDisposable
    {
        private readonly Lazy< IEmployeeRepository> _employeeRepository;
        private readonly Lazy< IDepartmentRepository> _departmentRepository;
        private readonly AppDbContext _appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            //Replace the dependency Injection with the lazy implementaion for the repository
            _appDbContext = appDbContext;
            _employeeRepository =new Lazy<IEmployeeRepository>(()=>new EmployeeRepository(_appDbContext));
            _departmentRepository =new Lazy<IDepartmentRepository>(()=>new DepartmentRepository(_appDbContext));
        }
        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

        public IDepartmentRepository DepartmentRepository => _departmentRepository.Value;
         

        //this interface to make sure that there are some things to be done before closing the connection
        public void Dispose()
        {
            
        }

        public int SaveChanges()
        {
            return _appDbContext.SaveChanges();
             
        }
    }
}
