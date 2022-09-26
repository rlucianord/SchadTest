using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Schad.DAL;
using Schad.Models.Data;

namespace Schad.Web.Models
{
    public class Customers : ICusromers
    {
        private DataModel ModeloDatos;

        public Customers()
        {
            ModeloDatos = new DataModel();
        }
        public void AddCustomer(Customer customer)
        {
            ModeloDatos.Customers.Add(customer);
            ModeloDatos.SaveChanges();
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> CustomersList()
        {
            return ModeloDatos.Customers.ToList();
        }

        public Customer GetCustomer(int Id)
        {

            return ModeloDatos.Customers.Find(Id);

        }

        public void UpdateCustomer(Customer customer)
        {
            ModeloDatos.Customers.Attach(customer);
            ModeloDatos.Entry(customer).State = System.Data.Entity.EntityState.Modified;
            ModeloDatos.SaveChanges();
        }
    }
}