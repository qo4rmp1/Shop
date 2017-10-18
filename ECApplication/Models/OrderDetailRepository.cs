using System;
using System.Linq;
using System.Collections.Generic;
	
namespace ECApplication.Models
{   
	public  class OrderDetailRepository : EFRepository<OrderDetail>, IOrderDetailRepository
	{

	}

	public  interface IOrderDetailRepository : IRepository<OrderDetail>
	{

	}
}