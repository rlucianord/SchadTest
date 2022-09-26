using Schad.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Schad.Web.Models
{
    public class InvoiceTotal
    {
        public int DiscountTotal
        {
            get
            {
                return InvoiceDetailsShows.Select(s => s.Discount).Sum();
            }
        }
        public decimal SubTotal {
            get
            {
                return (decimal)InvoiceDetailsShows.Select(s => s.Subtotal).Sum();
            }
        }
        public decimal TotalTaxTotal {
            get
            {
                return (decimal)InvoiceDetailsShows.Select(s => s.TaxAmount).Sum();
            }
        }
        public decimal TotalInvoice
        {
            get
            {
                return SubTotal + TotalTaxTotal;
            }
        }

        public decimal TotalTaxeSales
        {
            get
            {
                return (decimal)InvoiceDetailsShows
                    .Select(s => s.Subtotal).Sum() +
                    (decimal)InvoiceDetailsShows
                    .Select(s => s.TaxAmount).Sum();
            }
        }

        public decimal TotalTaxeExemptSales
        {
            get
            {
                return (decimal)InvoiceDetailsShows
                    .Where(w => w.ItemTaxFree == true)
                    .Select(s => s.Subtotal).Sum() + (decimal)InvoiceDetailsShows
                    .Where(w => w.ItemTaxFree == true)
                    .Select(s => s.TotalWTax).Sum();
            }
        }

        public List<InvoiceDetailsShow> InvoiceDetailsShows { get; set; }
    }
}