using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerApi.Data.Models;
using CustomerApi.Data.Models.Sql;

namespace CustomerApi.Data
{
    public class CustomerSqlRepository
    {
        private readonly CustomerDatabaseContext _context;

        public CustomerSqlRepository(CustomerDatabaseContext context)
        {
            _context = context;
        }

        public CustomerRecord Create(CustomerRecord customer)
        {
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<CustomerRecord> entry = _context.Customers.Add(customer);
            _context.SaveChanges();
            return entry.Entity;
        }
        public void Update(CustomerRecord customer)
        {
            _context.SaveChanges();
        }

        public void Remove(long id)
        {
            _context.Customers.Remove(GetById(id));
            _context.SaveChanges();
        }
        public IQueryable<CustomerRecord> GetAll()
        {
            return _context.Customers;
        }
        public CustomerRecord GetById(long id)
        {
            return _context.Customers.Find(id);
        }
    }
}
