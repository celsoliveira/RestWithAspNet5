using System;
using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Model;
using RestWithAspNet5.Model;
using RestWithAspNet5.Model.Base;
using RestWithAspNet5.Model.Context;

namespace RestWithAspNet5.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {

        private MySQLContext _context;
        private DbSet<T> dataSet;

        public GenericRepository(MySQLContext context)
        {
            _context = context;
            dataSet = _context.Set<T>();
        }

        public List<T> FindAll()
        {
            return dataSet.ToList();
        }

        public T FindById(long id)
        {
            return dataSet.SingleOrDefault(p => p.Id == id);
        }

        public T Create(T item)
        {
            try
            {
                dataSet.Add(item);
                _context.SaveChanges();
                return item;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public T Update(T item)
        {
            var result = dataSet.SingleOrDefault(p => p.Id.Equals(item.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            } else
            {
                return null;
            }
        }

        public void Delete(long id)
        {
            var result = dataSet.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    dataSet.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public bool Exists(long id)
        {
            return dataSet.Any(p => p.Id.Equals(id));
        }
    }
}
