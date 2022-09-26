using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Schad.DAL;
using Schad.Models.Data;
using static Schad.DAL.IInvoice;

namespace Schad.Web.Models
{
    public class Inventory : IInventory
    {
        private DataModel db = new DataModel();
        public void AddItem(Product inventory)
        {

            db.Products.Add(inventory);
            db.SaveChanges();
        }

        public void DeleteInventory(int IdItem)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InventoryViewModel> GetInventories(string name)
        {
            var list = db.Products                //.Where(i => (i.Id.ToString().Equals(name)) || (i.Description.Contains(name)))
                .Select(s => new InventoryViewModel()
                {

                    Department = "Dept Default",
                    ItemNum = s.Id.ToString(),
                    ItemName = s.Description,
                    Price = (decimal)s.Price,
                    In_Stock = (double)s.In_Stock
                })
                .OrderBy(o => o.ItemNum).ToList();

            if (!string.IsNullOrEmpty(name))
                list = list.Where(i => (i.ItemNum.Equals(name)) || (i.ItemName.Contains(name))).ToList();

            return list;
        }

        public void UpdatateInventory(Product inventory)
        {
            db.Products.Attach(inventory);
            db.Entry(inventory).State = EntityState.Modified;

            db.SaveChanges();
        }

        public IEnumerable<InventoryViewModel> SearchItems(string name)
        {
            var Inventory = db.Products                //.Where(i => (i.Id.ToString().Equals(name)) || (i.Description.Contains(name)))
                .Select(s => new InventoryViewModel()
                {
                    Department = "Dept Default",
                    ItemNum = s.Id.ToString(),
                    ItemName = s.Description,
                    Price = (decimal)s.Price,
                    In_Stock = (double)s.In_Stock
                })
                .OrderBy(o => o.ItemNum).ToList();

            if (!string.IsNullOrEmpty(name))
                Inventory = Inventory.Where(i => (i.ItemNum.Equals(name)) || (i.ItemName.Contains(name))).ToList();


            return Inventory;
        }

        public Product GetByID(string itemNumber)
        {
            return db.Products.FirstOrDefault(f => f.Id.ToString().Equals(itemNumber));
        }
        public List<Product> Notexists(string itemNumber)
        {
            return db.Products.Where(f => f.Id.ToString().Contains(itemNumber)).ToList();
        }
        public void ManageStock(string itemNumber, decimal stock)
        {
            var item = GetByID(itemNumber);

            UpdatateInventory(item);
        }

        public void ExecuteQuery(string q)
        {
            db.Database.ExecuteSqlCommand(q);
        }
    }

    public class InventoryViewModel
    {
        public string ItemNum { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public double In_Stock { get; set; }
        public int CustomerId { get; set; }
        public string Department { get; set; }
    }
    public class RestockViewModel
    {
        public string id { get; set; }
        public decimal value { get; set; }
    }
}