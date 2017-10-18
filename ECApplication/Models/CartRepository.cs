using System;
using System.Linq;
using System.Collections.Generic;
	
namespace ECApplication.Models
{   
	public  class CartRepository : EFRepository<Cart>, ICartRepository
	{

	}

	public  interface ICartRepository : IRepository<Cart>
	{

	}
}