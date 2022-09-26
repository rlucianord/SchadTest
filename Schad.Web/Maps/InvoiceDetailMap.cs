using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Schad.Models.Data;
namespace Schad.Web.Maps
{
    public class InvoiceDetailMap : IMap<List<InvoiceDetailsShow>, List<InvoiceDetail>>
    {
        public List<InvoiceDetail> ToMap(List<InvoiceDetailsShow> source)
        {
            DataModel db = new DataModel();
            var result = new List<InvoiceDetail>();
            foreach (var item in source)
            {
                result.Add(
                    new InvoiceDetail()
                    {
                        Id = item.LineNumber,
                        ItemNum=item.ItemNum,
                        Qty = (int)Math.Truncate ((decimal)item.Quantity),
                        Price = item.PricePer,
                        TotalItebis = (decimal)item.TaxAmount,
                        Subtotal= item.PricePer* (int)Math.Truncate((decimal)item.Quantity),
                        Total = (item.PricePer * (int)Math.Truncate((decimal)item.Quantity))+ (decimal)item.TaxAmount,
                    }
                    );;
            }
            return result;
        }
    }
}