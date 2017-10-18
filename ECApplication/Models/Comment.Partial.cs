namespace ECApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(CommentMetaData))]
    public partial class Comment
    {
    }
    
    public partial class CommentMetaData
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public System.DateTime CreateTime { get; set; }

        [Required]
        public string UserName{ get; set; }

        public Nullable<int> ProductId { get; set; }
        [UIHint("Star")]
        public Nullable<int> Star { get; set; }
    
        public virtual Member Member { get; set; }
        public virtual Product Product { get; set; }
    }
}
