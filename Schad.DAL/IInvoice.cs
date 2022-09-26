using System.Collections.Generic;
using Schad.Models.Data;

namespace Schad.DAL
{
    public class IInvoice
        {


        public interface IInvoices
            {
            void AddItem(Invoice InvoiceItemized);

            InvoiceDetail RecallInvoice(int? InvioceNumber);
            List<InvoiceDetail> GetInvoicesItems();
            long AddInvoiceTotal(Invoice InvoiceTotal);
            void UpdatateInvoice(InvoiceDetail InvoiceItemized);
            void DeleteInvoice(int idInvoice);
            
            void DeleteInvoiceHold(long Invoice_Hold);
            void Addlines(string IdProducto, List<InvoiceDetailsShow> listLines);
            void DeleteLine(string IdProducto, List<InvoiceDetailsShow> listLines);
            void ChangeQntLine(string IdProducto, List<InvoiceDetailsShow> listLines, int quantity);
            void ChangePrice(string IdProducto, List<InvoiceDetailsShow> listLines, decimal price);
            void ChangeDiscount(string IdProducto, List<InvoiceDetailsShow> listLines, int discount);
            long GenerateOrderNumber();

            }
        public interface IInventory
            {
            void AddItem(Product inventory);
            void UpdatateInventory(Product inventory);
            void DeleteInventory(int IdItem);
            Product GetByID(string itemNumber);
            void ManageStock(string itemNumber, decimal stock);
            }

        }
    public enum EstatusInvoice

        {
        Cerrado = 1, Devuelto, OnHold

        }


    }