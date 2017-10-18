using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECApplication.Models.ViewModels
{
    public class MemberLoginVM
    {
        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [DisplayName("帳號")]
        [Required(ErrorMessage = "請輸入帳號")]
        public string Username { get; set; }

        [StringLength(40, ErrorMessage = "欄位長度不得大於 40 個字元")]
        [DisplayName("密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}