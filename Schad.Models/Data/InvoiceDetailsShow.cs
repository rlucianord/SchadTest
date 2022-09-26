using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schad.Models.Data
{
    public partial class InvoiceDetailsShow
    {
        private decimal _price { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LineNumber { get; set; }
        public long? HoldID { get; set; }
        public string ItemDescripcion { get; set; }
        public string ItemNum { get; set; }
        public bool ItemTaxFree { get; set; }
        public int Discount { get; set; }
        public double Quantity { get; set; }
        public  double ItemTaxRate = 0.18;
        public decimal ItemPrice
        {
            get
            {
                return _price;
            }
        }
        public decimal PricePer
        {
            get { return ((100 - Discount) * _price) / 100; }
            set { _price = value; }
        }
        public double TaxAmount
        {
            get
            {
                return Math.Round((((double)PricePer * (ItemTaxRate)) * Quantity), 3);
            }
        }
        public double Subtotal
        {
            get
            {
                return Quantity * ((double)PricePer);
            }
        }
        public double TotalWTax
        {
            get
            {
                return Math.Round(Quantity * (double)PricePer + (((double)PricePer * (ItemTaxRate)) * Quantity), 3);
            }
        }
        public int IdFactura { get; set; }

    }
}

