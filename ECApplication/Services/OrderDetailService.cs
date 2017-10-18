using ECApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECApplication.Services
{
    public class OrderDetailService
    {
        OrderDetailRepository _repo_OrderDetail = RepositoryHelper.GetOrderDetailRepository();

        public OrderDetailService()
        {
            _repo_OrderDetail.UnitOfWork.LazyLoadingEnabled = false;
        }

        public void Add(OrderDetail orderdetail)
        {
            _repo_OrderDetail.Add(orderdetail);
        }

        public void Save()
        {
            _repo_OrderDetail.UnitOfWork.Commit();
        }

        public List<OrderDetail> GetOrderDetailByHeader(int headerId)
        {
            return _repo_OrderDetail.All().Where(p => p.OrderHeaderId == headerId).ToList();
        }
    }
}