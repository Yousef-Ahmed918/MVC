using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.DepartmentModels;
using DataAccess.Repositories.Interfaces;
using DataAcess.Contexts;

namespace DataAccess.Repositories.Classes
{
    public class DepartmentRepository(AppDbContext _dbContext) : 
        GenericRepository<Department>( _dbContext),
        IDepartmentRepository
    //Primary Constructor
    {
        

    }
}
