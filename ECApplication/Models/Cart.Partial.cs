namespace ECApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(CartMetaData))]
    public partial class Cart
    {
    }
    
    public partial class CartMetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Amount { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
