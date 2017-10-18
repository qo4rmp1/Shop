using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECApplication.Models.ViewModels
{
    public class MemberRegisterVM
    {
        [StringLength(20, ErrorMessage = "欄位長度不得大於 20 個字元")]
        [Required(ErrorMessage = "請輸入帳號")]
        public string UserName { get; set; }
        
        [StringLength(200, ErrorMessage = "欄位長度不得大於 200 個字元")]
        [Required(ErrorMessage = "請輸入Email")]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(40, ErrorMessage = "欄位長度不得大於 40 個字元")]
        [DisplayName("密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("確認密碼")]
        [Required(ErrorMessage = "請輸入確認密碼")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "兩次密碼輸入不一致")]
        public string PasswordCheck { get; set; }
    }
}