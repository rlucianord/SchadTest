using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Schad.DAL;
using Schad.Models.Data;

namespace Schad.Web.Models
{
    public class Customers : ICustomers
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
            
        }

        public void AddCustomerType(CustomerType customerType)
        {

            ModeloDatos.CustomerTypes.Add(customerType);
            ModeloDatos.SaveChanges();

        }

        public IEnumerable<Customer> CustomersList()
        {
            return ModeloDatos.Customers.ToList();
        }

        public IEnumerable<CustomerType> CustomerTypes()
        {
            return ModeloDatos.CustomerTypes.ToList();
        }

        public Customer GetCustomer(int Id)
        {

            return ModeloDatos.Customers.Find(Id);

        }

        public CustomerType GetCustomerType(int Id)
        {
            return ModeloDatos.CustomerTypes.Find(Id);
        }

        public void UpdateCustomer(Customer customer)
        {
            ModeloDatos.Customers.Attach(customer);
            ModeloDatos.Entry(customer).State = System.Data.Entity.EntityState.Modified;
            ModeloDatos.SaveChanges();
        }

        public void UpdateCustomerType(CustomerType customer)
        {
            ModeloDatos.CustomerTypes.Attach(customer);
            ModeloDatos.Entry(customer).State = System.Data.Entity.EntityState.Modified;
            ModeloDatos.SaveChanges();
        }
    }
}