namespace ECApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ProductCategoryMetaData))]
    public partial class ProductCategory
    {
    }

    [DisplayName("商品分類")]
    public partial class ProductCategoryMetaData
    {
        [Required]
        public int Id { get; set; }
        
        [StringLength(100, ErrorMessage="欄位長度不得大於 100 個字元")]
        public string Name { get; set; }
    
        public virtual ICollection<Product> Product { get; set; }
    }
}
