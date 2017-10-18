using System;
using System.Linq;
using System.Collections.Generic;
	
namespace ECApplication.Models
{   
	public  class OrderHeaderRepository : EFRepository<OrderHeader>, IOrderHeaderRepository
	{

	}

	public  interface IOrderHeaderRepository : IRepository<OrderHeader>
	{

	}
}