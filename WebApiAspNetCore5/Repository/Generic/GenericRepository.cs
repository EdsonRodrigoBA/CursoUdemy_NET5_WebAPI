using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiAspNetCore5.Models.Base;
using WebApiAspNetCore5.Models.Context;
namespace WebApiAspNetCore5.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly MySqlContext _mysqlContext;

        private DbSet<T> dbSet;
        public GenericRepository(MySqlContext mysqlContext)
        {
            this._mysqlContext = mysqlContext;
            dbSet = _mysqlContext.Set<T>();
        }


        public T Create(T model)
        {
            try
            {
                dbSet.Add(model);
                _mysqlContext.SaveChanges();
                return model;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }
        public T Update(T model)
        {
            try
            {
                var result = dbSet.SingleOrDefault(p => p.id.Equals(model.id));
                if (result != null)
                {
                    _mysqlContext.Entry(result).CurrentValues.SetValues(model);
                    _mysqlContext.SaveChanges();
                    return result;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Delete(long id)
        {
            try
            {
                var result = dbSet.SingleOrDefault(p => p.id.Equals(id));
                if (result != null)
                {
                    dbSet.Remove(result);
                    _mysqlContext.SaveChanges();

                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public List<T> FindAll()
        {
           return dbSet.ToList();
        }

        public T FindByID(long id)
        {
            return dbSet.SingleOrDefault(p => p.id == id);
        }



        public bool Exists(long id)
        {
            return dbSet.Any(p => p.id.Equals(id));

        }
    }
}
