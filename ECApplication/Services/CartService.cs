using ECApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECApplication.Services
{
    public class CartService
    {
        CartRepository _repo_Cart = RepositoryHelper.GetCartRepository();

        public CartService()
        {

        }

        public Cart GetCartByProductId(int productId)
        {
            return _repo_Cart.All().Where(p => p.ProductId == productId).FirstOrDefault();
        }

        public void AddCart(Cart cart)
        {
            _repo_Cart.Add(cart);
        }

        public void Remove(Cart cart)
        {
            _repo_Cart.Delete(cart);
        }
    }
}