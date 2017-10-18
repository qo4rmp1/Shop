namespace ECApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(OrderHeaderMetaData))]
    public partial class OrderHeader
    {
    }

    [DisplayName("收件人資料")]
    public partial class OrderHeaderMetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        
        [StringLength(40, ErrorMessage="欄位長度不得大於 40 個字元")]
        [Required]
        [DisplayName("姓名")]
        public string ContactName { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        [DisplayName("手機")]
        public string ContactPhoneNo { get; set; }
        
        [StringLength(100, ErrorMessage="欄位長度不得大於 100 個字元")]
        [Required]
        [DisplayName("地址")]
        public string ContactCity { get; set; }
        
        [StringLength(100, ErrorMessage="欄位長度不得大於 100 個字元")]
        [Required]
        [DisplayName("  ")]
        public string ContactStreet1 { get; set; }
        
        [Required]
        [DisplayName("金額")]
        public int TotalPrice { get; set; }

        [DisplayName("備註")]
        public string Memo { get; set; }
        public Nullable<System.DateTime> BuyOn { get; set; }
    
        public virtual Member Member { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
