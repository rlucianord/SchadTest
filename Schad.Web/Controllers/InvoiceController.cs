using Schad.Models.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using Schad.Web.Maps;
using Schad.Web.Models;

namespace Schad.Web.Controllers
{
    //[Authorize, OutputCache(NoStore = true, Duration = 0, Location = System.Web.UI.OutputCacheLocation.None)]
    public class InvoiceController : Controller
    {
        public string directory;
        private DataModel db = new DataModel();
        public List<InvoiceDetailsShow> ListDetails = new List<InvoiceDetailsShow>();
        public InvoiceItemized invoice = new InvoiceItemized();
        Inventory inventory = new Inventory();

        //readonly string rutaimage;

        //static int Quantity = 0;


        public ActionResult Inndex()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "CustName");

            return RedirectPermanent("Create");
        }
        public void Invoice()
        {
            ListDetails = new List<InvoiceDetailsShow>();


        }

        public class DataResult
        {
            public IEnumerable<object> result { get; set; }
            public int Count { get; set; }
        }


        public ActionResult Index()
        {
            Invoice invoice_Totals = new Invoice();
            Session["inventory"] = true;
            ViewBag.CustomerId = new SelectList(db.Customers.ToList(), "Id", "CustName");

            return RedirectToActionPermanent("Create");
            //   PartialView("_Create", invoice_Totals);
        }
        //public ActionResult DataSource(Syncfusion.EJ2.DataManager dm)
        //{
        //    var DataSource = ListDetails;
        //    DataResult result = new DataResult();
        //    try
        //    {
        //        result.result = DataSource;
        //        result.Count = DataSource.Count;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        //

        [AcceptVerbs("POST")]
        public JsonResult DeleLine(string Idproducto)
        {
            invoice.DeleteLine(Idproducto, (List<InvoiceDetailsShow>)Session["Invoice"]);
            return Json((List<InvoiceDetailsShow>)Session["Invoice"], JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs("POST")]
        public JsonResult Addlines(string IdProducto)
        {
            invoice.Addlines(IdProducto, (List<InvoiceDetailsShow>)Session["Invoice"]);

            return Json((List<InvoiceDetailsShow>)Session["Invoice"], JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs("POST")]
        public JsonResult ChangeQnty(string IdProducto, int quantity)
        {
            invoice.ChangeQntLine(IdProducto, (List<InvoiceDetailsShow>)Session["Invoice"], quantity);
            return Json((List<InvoiceDetailsShow>)Session["Invoice"], JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs("POST")]
        public JsonResult ChangePrice(string IdProducto, decimal price)
        {
            invoice.ChangePrice(IdProducto, (List<InvoiceDetailsShow>)Session["Invoice"], price);
            return Json((List<InvoiceDetailsShow>)Session["Invoice"], JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs("POST")]
        public JsonResult ChangeDiscount(string IdProducto, int discount)
        {
            invoice.ChangeDiscount(IdProducto, (List<InvoiceDetailsShow>)Session["Invoice"], discount);
            return Json((List<InvoiceDetailsShow>)Session["Invoice"], JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs("POST")]

        public JsonResult ClearLines()
        {
            var list = (List<InvoiceDetailsShow>)Session["Invoice"];
            ListDetails = new List<InvoiceDetailsShow>();
            Session["Invoice"] = ListDetails;
            ViewBag.CustomerId = new SelectList(db.Customers.ToList(), "Id", "CustName");
            return Json(ListDetails, JsonRequestBehavior.AllowGet);


        }


        [AcceptVerbs("POST")]
        public ActionResult Delete(int key)
        {
            var detalles = ListDetails.Where(x => x.LineNumber == key).FirstOrDefault();
            try
            {
                ListDetails.Remove(detalles);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            var data = ListDetails;
            return Json(data, JsonRequestBehavior.AllowGet);
        }







        [HttpPost]
        public JsonResult AddInvoice(decimal AmtChange, int PaymentMethod, int CustomerId = 0, bool print = true)
        {
            print = true;
            var list = (List<InvoiceDetailsShow>)Session["Invoice"];
            InvoiceItemized.Station_ID = "";

            var invoiceTotal = new InvoiceTotal();
            invoiceTotal.InvoiceDetailsShows = list;
            var msg = invoice.AddInvoice(
                new Invoice()
                {
                    TotalItbis = invoiceTotal.TotalTaxTotal,
                    Subtotal = invoiceTotal.SubTotal,
                    Total = invoiceTotal.TotalTaxeSales,
                    CustomerId = CustomerId

                },
                new InvoiceDetailMap().ToMap(invoiceTotal.InvoiceDetailsShows), print
                );

            ViewBag.CustomerId = new SelectList(db.Customers.ToList(), "Id", "CustName");
            return Json(msg);
        }

        // GET: Cotizacion

        [HttpGet]
        public ActionResult Create()
        {
            Session["Invoice"] = null;

            ViewBag.CustomerId = new SelectList(db.Customers.ToList(), "Id", "CustName");

            Invoice encababezadoFact = new Invoice();
            ViewBag.DataSourse = ListDetails;
            Session["Invoice"] = ListDetails;


            ViewBag.Total = 0;
            return View(encababezadoFact);
        }


        [HttpPost]

        public ActionResult Index(string name, bool search)
        {
            if (name == "")
            {
                name = null;
            }
            var data = inventory.SearchItems(name).ToList();
            ViewBag.CustomerId = new SelectList(db.Customers.ToList(), "Id", "CustName");

            ViewBag.Search = search;
            return PartialView("_Index", data);
        }

        [HttpPost]
        public ActionResult Payment()
        {
            var list = (List<InvoiceDetailsShow>)Session["Invoice"];
            ViewBag.CustomerId = new SelectList(db.Customers.ToList(),"Id", "CustName");

            return PartialView("_Payment");
        }



    }
}
