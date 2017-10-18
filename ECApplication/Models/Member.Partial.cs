namespace ECApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Security;

    [MetadataType(typeof(MemberMetaData))]
    public partial class Member
    {
        // 密碼加密
        public void SetHashPassword()
        {
            this.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(this.Password, "SHA1");
        }
    }

    public partial class MemberMetaData
    {
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [StringLength(40, ErrorMessage="欄位長度不得大於 40 個字元")]
        [Required]
        public string Password { get; set; }
        public Nullable<System.DateTime> RegisterOn { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string AuthCode { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string UserName { get; set; }

        [Required]
        public bool IsAdmin { get; set; }
    
        public virtual ICollection<OrderHeader> OrderHeader { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
    }
}
