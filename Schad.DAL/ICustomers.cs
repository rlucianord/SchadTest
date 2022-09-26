using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schad.Models.Data;

namespace Schad.DAL
{
    public  interface ICustomers
    {
        void AddCustomer(Customer customer);
        Customer GetCustomer(int Id);
        void UpdateCustomer(Customer customer);
        IEnumerable<Customer> CustomersList();
        IEnumerable<CustomerType> CustomerTypes();
        CustomerType GetCustomerType(int Id);
        void AddCustomerType(CustomerType customerType);
        void UpdateCustomerType(CustomerType customerType);

    }

}
