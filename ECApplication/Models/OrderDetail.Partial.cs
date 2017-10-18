namespace ECApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(OrderDetailMetaData))]
    public partial class OrderDetail
    {
    }
    
    public partial class OrderDetailMetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int OrderHeaderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Amount { get; set; }

        public virtual OrderHeader OrderHeader { get; set; }
        public virtual Product Product { get; set; }
    }
}
