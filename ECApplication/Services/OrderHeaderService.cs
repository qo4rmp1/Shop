using ECApplication.Models;
using System.Linq;
using System;
using System.Collections.Generic;

namespace ECApplication.Services
{
    public class OrderHeaderService
    {
        OrderHeaderRepository _repo = RepositoryHelper.GetOrderHeaderRepository();

        public OrderHeaderService()
        {
            _repo.UnitOfWork.LazyLoadingEnabled = false;
        }

        public void Add(OrderHeader orderheader)
        {
            _repo.Add(orderheader);
        }

        public void Save()
        {
            _repo.UnitOfWork.Commit();
        }

        /// <summary>
        /// 取得最新一筆訂單
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public List<OrderHeader> GetOrderHeaderByMember(string username)
        {
            return _repo.All().Where(p => p.UserName.Equals(username)).OrderByDescending(p => p.Id).ToList();
            // debug
            //return _repo.All().Where(p => p.MemberId == memberId).OrderBy(p => p.Id).LastOrDefault();
        }
        
        public bool AlreadyBuy(int productId, string userName)
        {
            _repo.UnitOfWork.LazyLoadingEnabled = true;

            try
            {
                if (_repo.All().Any(p => p.UserName == userName) == false)
                {
                    return false;
                }

                return _repo.All().Any(p => p.OrderDetail.Any(q => q.ProductId == productId));
            }
            finally
            {
                _repo.UnitOfWork.LazyLoadingEnabled = false;
            }
        }

        public OrderHeader GetOrderHeader(int id)
        {
            return _repo.All().Where(p => p.Id == id).FirstOrDefault();
        }
    }
}