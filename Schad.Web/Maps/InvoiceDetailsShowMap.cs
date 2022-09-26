using Schad.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Schad.Web.Maps
{
    public class InvoiceDetailsShowMap : IMap<List<InvoiceDetail>, List<InvoiceDetailsShow>>
    {
        public List<InvoiceDetailsShow> ToMap(List<InvoiceDetail> source)
        {
            DataModel db = new DataModel();
            var result = new List<InvoiceDetailsShow>();
            foreach (var item in source)
            {
                result.Add(
                    new InvoiceDetailsShow()
                    {
                        LineNumber = item.Id,
                        ItemNum = item.ProductId.ToString(),
                        ItemDescripcion = item.Product.Description,
                        PricePer = item.Price,
                        ItemTaxRate = 0.18,
                        Quantity = (double)item.Qty,
                        ItemTaxFree = item.TotalItebis > 0 ? false : true,
                        Discount = 0
                    }
                    );
            }
            return result;
        }
    }
}