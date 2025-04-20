using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        #region Get
        //Get All
        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            if (withTracking)
            {
                return _dbContext.Set<TEntity>().Where(e => e.IsDeleted != true).ToList();
            }
            else
                return _dbContext.Set<TEntity>().Where(e => e.IsDeleted != true).AsNoTracking().ToList();
        }
        public IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector)
        {

            return _dbContext.Set<TEntity>()
                .Select(selector).ToList() ;

        }
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            return _dbContext.Set<TEntity>()
                .Where(e => e.IsDeleted != true)
                .Where(filter).ToList();
        }
        //GetById
        public TEntity? GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        } 
        #endregion
        //Add
        public void Add(TEntity entity)
        {
            _dbContext.Set <TEntity>().Add(entity);
        }
        //Edit
        public void Update(TEntity entity)
        {
            _dbContext.Set <TEntity>().Update(entity);
        }
        //Delete
        public void Remove(TEntity entity)
        {
            _dbContext.Set <TEntity>().Remove(entity);
        }

        
        
    }
}
