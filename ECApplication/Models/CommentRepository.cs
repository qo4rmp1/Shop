using System;
using System.Linq;
using System.Collections.Generic;
	
namespace ECApplication.Models
{   
	public  class CommentRepository : EFRepository<Comment>, ICommentRepository
	{

	}

	public  interface ICommentRepository : IRepository<Comment>
	{

	}
}