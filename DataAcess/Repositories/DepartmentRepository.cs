using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess.Contexts;

namespace DataAcess.Repositories
{
    public class DepartmentRepository(AppDbContext dbContext) : IDepartmentRepository
    //Primary Constructor
    {
        private readonly AppDbContext _dbContext = dbContext;



        //CRUD Operations
        //Get All
        public IEnumerable<Department> GetAll(bool withTracking = false)
        {
            if (withTracking)
            {
                return _dbContext.Departments.ToList();
            }
            else
                return _dbContext.Departments.AsNoTracking().ToList();
        }
        //GetById
        public Department? GetById(int id)
        {
            return _dbContext.Departments.Find(id);
        }
        //Add
        public int Add(Department dept)
        {
            _dbContext.Departments.Add(dept);
            return _dbContext.SaveChanges();
        }
        //Edit
        public int Update(Department dept)
        {
            _dbContext.Departments.Update(dept);
            return _dbContext.SaveChanges();
        }
        //Delete
        public int Remove(Department dept)
        {
            _dbContext.Departments.Remove(dept);
            return _dbContext.SaveChanges();
        }

    }
}
