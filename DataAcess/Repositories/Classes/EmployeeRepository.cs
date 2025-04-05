using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repositories.Interfaces;
using DataAcess.Contexts;

namespace DataAccess.Repositories.Classes
{

    public class EmployeeRepository(AppDbContext _dbContext) :
        GenericRepository<Employee>(_dbContext),
        IEmployeeRepository
    //Primary Constructor
    {
        
    }
}

