
using Schad.DAL;
using Schad.Web.Controllers;
using Schad.Web.Maps;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using Schad.Models.Data;
using static Schad.DAL.IInvoice;

namespace Schad.Web.Models
{

    public class InvoiceItemized : IInvoices
    {
        private DataModel modeloDatos = new DataModel();
        Product Inventory = new Product();
        public static string Station_ID;
        private SerialPort comPort = null;
        private byte[] queryCommand = new byte[] { 0x10, 0x04, 0x01 }; // DLE, EOT, 1
        private const int timeout = 5000;
        private const int STATUS_LENGTH = 1;
        public static int Cashier_ID;
        public static string[] PrintLines;
        public static string msnorder;
        // string PrintPort = @HomeController.companies.FirstOrDefault().PortName.Trim();




        private bool GetPrinterStatus(out byte[] status)
        {

            Handshake handshake = comPort.Handshake;
            comPort.Handshake = Handshake.None;

            try
            {
                comPort.Write(queryCommand, 0, queryCommand.Length);

                Thread.Sleep(200);

                byte[] readBuffer = new byte[STATUS_LENGTH];
                int readSize = comPort.Read(readBuffer, 0, readBuffer.Length);

                if (readSize != STATUS_LENGTH)
                {
                    //    Logging("Read failed.");
                    status = null;
                    return false;
                }

                status = readBuffer;
                // ShowPrinterStatus(status);

                return true;
            }
            catch (Exception ex)
            {
                status = null;
                return false;
            }
            finally
            {
                //EN: Reset flow control to enable from disable
                //JP: フロー制御を元に戻す
                comPort.Handshake = handshake;
            }
        }
        public string AddInvoice(Invoice InvoiceTotal, List<InvoiceDetail> InvoiceItemized, bool print)
        {
            string result = "";
            long orderNumber = 0;
            try
            {
                 orderNumber = AddInvoiceTotal(InvoiceTotal);

               
                foreach (var item in InvoiceItemized)
                {
                    item.InvoiceId = orderNumber;
                    item.ProductId = int.Parse(item.ItemNum);
                  
                    AddItem(item);
                }



            }


            catch (Exception e)
            {
                if (e.GetBaseException().GetType().FullName == "System.UnauthorizedAccessException")
                {
                    result = string.Empty.Trim();
                    
                }
                var invoidedelete = modeloDatos.Invoices.First(c => c.Id == orderNumber);
                if (invoidedelete != null)
                {
                    modeloDatos.Invoices.Remove(invoidedelete);
                    modeloDatos.SaveChanges();


                }
                else

                    result = e.Message;
            }
            return result;
        }

  
        //switch (estatusInvoice)
        //                  private string Invoesestatus(EstatusInvoice estatusInvoice)

        //        {
        //            string rt = string.Empty;
        //              {
        //                case DAL.EstatusInvoice.Cerrado:
        //                    rt = "C";
        //                    break;

        //                case DAL.EstatusInvoice.Devuelto:
        //                    rt = "R";
        //                    break;

        //                case DAL.EstatusInvoice.OnHold:
        //                    rt = "H";
        //                    break;
        //            }

        //            return rt;
        //        }



        public long AddInvoiceTotal(Invoice InvoiceTotal)
        {


            InvoiceTotal.Id = (int)GenerateOrderNumber();

            InvoiceTotal.DateCreated = DateTime.Now;
            //InvoiceTotal.Total = 0;
            //InvoiceTotal.Subtotal = 0;
            //InvoiceTotal.TotalItbis = 0;


            modeloDatos.Invoices.Add(InvoiceTotal);
            modeloDatos.SaveChanges();

            msnorder = "New order created, with the number" + InvoiceTotal.Id;


            return InvoiceTotal.Id;
        }

        public void AddItem(InvoiceDetail InvoiceItemized)
        {
            
            modeloDatos.InvoiceDetails.Add(InvoiceItemized);
            modeloDatos.SaveChanges();
        }

        public void Addlines(string IdProducto, List<InvoiceDetailsShow> listLines)
        {
            bool isItemPresent = listLines.Any(l => l.ItemNum == IdProducto);
            double quantity = isItemPresent ? listLines.FirstOrDefault(l => l.ItemNum == IdProducto).Quantity + 1 : 1;
            InvoiceDetailsShow invoiceDetailsShow = new InvoiceDetailsShow();
            if (isItemPresent)
            {
                var index = listLines.FindIndex(l => l.ItemNum == IdProducto);
                listLines[index].Quantity = quantity;
                //listLines[index].Subtotal = quantity * ((double)listLines[index].PricePer - listLines[index].TaxAmount);
                //listLines[index].TotalWTax = quantity * listLines[index].TaxAmount;
            }
            else
            {
                invoiceDetailsShow = (
                from c in modeloDatos.Products
                where c.Id.ToString().Equals(IdProducto)
                select new InvoiceDetailsShow()
                {
                    ItemNum = c.Id.ToString(),
                    ItemDescripcion = c.Description,
                    PricePer = (decimal)c.Price,
                    ItemTaxRate = 0.18,
                    Quantity = quantity,
                    ItemTaxFree = false,
                    Discount = 0
                }).FirstOrDefault();
                listLines.Add(invoiceDetailsShow);

                
            }
        }

        public void DeleteLine(string IdProducto, List<InvoiceDetailsShow> listLines)
        {
            var detalles = listLines.Where(x => x.ItemNum == IdProducto).FirstOrDefault();
            listLines.Remove(detalles);
        }

        public void ChangeQntLine(string IdProducto, List<InvoiceDetailsShow> listLines, int quantity)
        {
            var index = listLines.FindIndex(l => l.ItemNum == IdProducto);
            listLines[index].Quantity = quantity;
            //listLines[index].Subtotal = listLines[index].Quantity * ((double)listLines[index].PricePer - listLines[index].TaxAmount);
        }

        public void ChangePrice(string IdProducto, List<InvoiceDetailsShow> listLines, decimal price)
        {
            var index = listLines.FindIndex(l => l.ItemNum == IdProducto);
            listLines[index].PricePer = price;
            //listLines[index].Subtotal = listLines[index].Quantity * ((double)listLines[index].PricePer - listLines[index].TaxAmount);
        }

        public void ChangeDiscount(string IdProducto, List<InvoiceDetailsShow> listLines, int discount)
        {
            var index = listLines.FindIndex(l => l.ItemNum == IdProducto);
            listLines[index].Discount = discount;
            //listLines[index].Subtotal = listLines[index].Quantity * ((double)listLines[index].PricePer - listLines[index].TaxAmount);
        }

        public void DeleteInvoice(int IdAno)
        {
            throw new NotImplementedException();
        }

      
        public List<InvoiceDetail> GetInvoicesItems()
        {
            throw new NotImplementedException();
        }

        public InvoiceDetail RecallInvoice(int? InvioceNumber)
        {
            throw new NotImplementedException();
        }

        public void UpdatateInvoice(Invoice InvoiceItemized)
        {
            throw new NotImplementedException();
        }

        public long GenerateOrderNumber()
        {
            long initialNumber = 100000;
            long lastNumber = 0;
            var lastItem = modeloDatos.Invoices.OrderByDescending(f => f.Id).FirstOrDefault();
            if (lastItem != null)
            {
                lastNumber = lastItem.Id;
            }

            return lastNumber > 0 ? ++lastNumber : initialNumber;
        }
        public List<InvoiceDetailsShow> GetInvoicesItems(long idInvoice)
        {
            var invoicedet = modeloDatos.InvoiceDetails.Where(d => d.InvoiceId == idInvoice ).ToList();
            var order = new InvoiceDetailsShowMap().ToMap(invoicedet);
            return order;
        }

        private void DeleteInvoiceItemDetail(int item)
        {
            var detail = modeloDatos.InvoiceDetails.Find(item);
            modeloDatos.Entry(detail).State = EntityState.Deleted;
           
       modeloDatos.SaveChanges();
        }

      

        
        public void DeleteInvoiceTotal(long idTotal)
        {
                foreach (var item in modeloDatos.InvoiceDetails.Where(d => d.InvoiceId == idTotal).ToList())
                {
                    DeleteInvoiceItemDetail(item.Id);
                }
                   }

        void IInvoice.IInvoices.AddItem(Invoice InvoiceItemized)
        {
            throw new NotImplementedException();
        }

        void IInvoice.IInvoices.UpdatateInvoice(InvoiceDetail InvoiceItemized)
        {
            throw new NotImplementedException();
        }

        void IInvoice.IInvoices.DeleteInvoiceHold(long Invoice_Hold)
        {
            throw new NotImplementedException();
        }
    }

}