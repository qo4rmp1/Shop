using ECApplication.Models;
using System.Collections.Generic;

namespace ECApplication.Models.ViewModels
{
    public class OrderHistoryVM
    {
        public OrderHeader Oh;
        public List<OrderDetail> Ods;

        public OrderHistoryVM()
        {
            Oh = new OrderHeader();
            Ods = new List<OrderDetail>();
        }
    }
}