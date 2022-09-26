namespace Schad.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class InvoiceDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public long InvoiceId { get; set; }

        public int Qty { get; set; }

        public decimal Price { get; set; }

        public int ProductId { get; set; }

        public decimal? TotalItebis { get; set; }

        public decimal? Subtotal { get; set; }

        public decimal? Total { get; set; }

        public DateTime? DeteCreated { get; set; }

        public virtual Product Product { get; set; }

        public virtual Invoice Invoice { get; set; }
        [NotMapped]
        public string ItemNum { get; set; }
    }
}
