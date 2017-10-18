using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ECApplication.Models.ViewModels
{
    public class ChangePasswordVM
    {
        [StringLength(40, ErrorMessage = "欄位長度不得大於 40 個字元")]
        [DisplayName("舊密碼")]
        [Required(ErrorMessage = "請輸入舊密碼")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(40, ErrorMessage = "欄位長度不得大於 40 個字元")]
        [DisplayName("新密碼")]
        [Required(ErrorMessage = "請輸入新密碼")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [StringLength(40, ErrorMessage = "欄位長度不得大於 40 個字元")]
        [DisplayName("確認密碼")]
        [Required(ErrorMessage = "請輸入確認密碼")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "密碼輸入不一致")]
        public string NewPasswordCheck { get; set; }
    }
}