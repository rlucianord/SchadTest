using Schad.Models.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Schad.Web.Controllers
{
    
    public class InventoryController : Controller
    {
        Models.Inventory inventory = new Models.Inventory();
        // GET: Inventory
        public ActionResult Index(string name = "a")
        {

            var data = inventory.GetInventories(name).ToList();
            return PartialView("_Index", data);


        }
        public ActionResult IndexInventory(string SearchString = "a")
        {

            var data = inventory.GetInventories(SearchString).ToList();

            return View(data);


        }

        [HttpGet]
        public ActionResult Create()
        {
            Product inventoryCreate = new Product();
            return PartialView("_Create");


        }

        [HttpPost]
        // [ValidateAntiForgeryToken]

        public ActionResult Create(Product inventoryCreate)
        {


            string result = null;
            long invcount = 100000;



            //if (ModelState.IsValid)
            try
            {

                

                if (ModelState.IsValid)
                {
                    
                    inventory.AddItem(inventoryCreate);
                }


                else { result = "Error, Complete de form"; return View(inventoryCreate); }
            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.InnerException.InnerException;
                if (sqlException != null)
                {

                    result = "An error has occurred! Message: " + sqlException.Message;
                    //violation of primary key constraint has occurred here, act accordingly
                    //e.g. pass the populated viewmodel back to the view and return
                }
            }
            catch (Exception e)
            {
                result = "An error has occurred! Message: " + e.Message;
            }

            //  result = "False"+".We coulden't create the productr"+;
            return Json(result);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = inventory.GetByID(id);



            return PartialView("_Edit", item);
        }

        [HttpPost]
        public ActionResult Edit(Product inventoryEdit)
        {
            string result = null;
            
            try
            {



                inventory.UpdatateInventory(inventoryEdit);



            }

            catch (Exception e)
            {

                //ViewBag.Dept_ID = new SelectList(department.GetDepartaments(), "Dept_ID", "Description", inventoryEdit.Dept_ID);
                //ViewBag.TaxRate = new SelectList(taxes.GetTaxtes(), "Id_Tax", "Tax_Name", inventoryEdit.TaxRate);
                //
                result = "Error, Complete de form" + e.Message; return View(inventoryEdit);
            }


            return Json(result);
        }
        [HttpGet]
        public ActionResult EditInventory(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var item = inventory.GetByID(id);
            //ViewBag.Departamentos = new SelectList(department.GetDepartaments(), "Dept_ID", "Description", item.Dept_ID);
            //ViewBag.TaxRate = new SelectList(taxes.GetTaxtes(), "Id_Tax", "Tax_Name", item.TaxRate);
            return PartialView("_Edit", item);
        }

        public ActionResult InventoryStocks(string name = " ")
        {
            var data = inventory.GetInventories(name).ToList();
            return PartialView("_InventoryStocks", data);
        }
        public ActionResult InventoryStock(string name = " ")
        {
            var data = inventory.GetInventories(name).ToList();
            return View(data);
        }
        [HttpPost]
        public ActionResult InventoryAllStocks(int restock)
        {
            string result = null;
            try
            {
                //   inventory.ExecuteQuery("UPDATE Inventory SET In_Stock = (In_Stock + " + restock + ")");
                string str = string.Format("Exec [P_UpdateStock]  '{0}'", restock);
                inventory.ExecuteQuery(str);
            }
            catch (Exception e)
            {
                result = "An error has occurred! Message: " + e.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public ActionResult InventoryStocks(Models.RestockViewModel[] data)
        {
            string result = null;
            try
            {
                foreach (var item in data)
                {
                    inventory.ManageStock(item.id, item.value);
                }
            }
            catch (Exception e)
            {
                result = "An error has occurred! Message: " + e.Message;
            }
            return Json(result);
        }
    }
}