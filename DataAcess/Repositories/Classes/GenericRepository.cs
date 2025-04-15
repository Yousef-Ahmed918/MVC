using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.SharedModels;
using DataAccess.Repositories.Interfaces;
using DataAcess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Classes
{
    public class GenericRepository<TEntity>(AppDbContext _dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {

        //CRUD Operations
        //Get All
        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            if (withTracking)
            {
                return _dbContext.Set <TEntity>().Where(e=>e.IsDeleted!=true).ToList();
            }
            else
                return _dbContext.Set <TEntity>().Where(e => e.IsDeleted != true).AsNoTracking().ToList();
        }
        //GetById
        public TEntity? GetById(int id)
        {
            return _dbContext.Set <TEntity>().Find(id);
        }
        //Add
        public int Add(TEntity entity)
        {
            _dbContext.Set <TEntity>().Add(entity);
            return _dbContext.SaveChanges();
        }
        //Edit
        public int Update(TEntity entity)
        {
            _dbContext.Set <TEntity>().Update(entity);
            return _dbContext.SaveChanges();
        }
        //Delete
        public int Remove(TEntity entity)
        {
            _dbContext.Set <TEntity>().Remove(entity);
            return _dbContext.SaveChanges();
        }
    }
}
