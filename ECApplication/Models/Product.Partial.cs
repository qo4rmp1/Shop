namespace ECApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
    }
    
    [DisplayName("商品")]
    public partial class ProductMetaData
    {
        [Required]
        [DisplayName("編號")]
        public int ProductId { get; set; }
        
        [StringLength(80, ErrorMessage="欄位長度不得大於 80 個字元")]
        [DisplayName("名稱")]
        public string ProductName { get; set; }
        [DisplayName("價錢")]
        public int Price { get; set; }
        [UIHint("Active")]
        [DisplayName("是否上架")]
        public bool Active { get; set; }
        [DisplayName("存貨")]
        public int Stock { get; set; }
        [UIHint("CategoryId")]
        [DisplayName("分類")]
        public Nullable<int> CategoryId { get; set; }
        
        [StringLength(500, ErrorMessage="欄位長度不得大於 500 個字元")]
        [UIHint("Description")]
        [DisplayName("描述")]
        public string Description { get; set; }

        [DisplayName("商品圖片")]
        public byte[] PhotoFile { get; set; }
        
        [StringLength(100, ErrorMessage="欄位長度不得大於 100 個字元")]
        [DisplayName("圖片檔案")]
        public string ImageMimeType { get; set; }
        [DisplayName("圖片位址")]
        public string Url { get; set; }
        [DisplayName("圖片大小")]
        public Nullable<int> Size { get; set; }
        [DisplayName("圖片檔名")]
        public string FileName { get; set; }

        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<Cart> Cart { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
    }
}
