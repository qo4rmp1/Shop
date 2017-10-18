using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECApplication.Models.ViewModels
{
    public class EditProductVM
    {
        [Required]
        public int ProductId { get; set; }

        [StringLength(80, ErrorMessage = "欄位長度不得大於 80 個字元")]
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
        public decimal Stock { get; set; }
        [UIHint("Categorys")]
        public Nullable<int> CategoryId { get; set; }

        [StringLength(500, ErrorMessage = "欄位長度不得大於 500 個字元")]
        [UIHint("Description")]
        public string Description { get; set; }

        [DisplayName("圖片")]
        [UIHint("Image")]
        [FileExtensions(ErrorMessage ="所上傳檔案不是圖片")]
        public HttpPostedFile Image { get; set; }
    }
}