using ECApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECApplication.Services
{
    public class ProductsService
    {
        ProductRepository _repo = RepositoryHelper.GetProductRepository();
        ProductCategoryRepository _repo_Category;
        CommentRepository _repo_Comment;

        public ProductsService()
        {
            _repo_Category = RepositoryHelper.GetProductCategoryRepository(_repo.UnitOfWork);
            _repo_Comment = RepositoryHelper.GetCommentRepository(_repo.UnitOfWork);

            //_repo.UnitOfWork.LazyLoadingEnabled = false;
            //_repo_Category.UnitOfWork.LazyLoadingEnabled = false;
            //_repo_Comment.UnitOfWork.LazyLoadingEnabled = false;
        }

        public List<ProductCategory> GetCategory()
        {
            return _repo_Category.All().ToList();
        }

        public IQueryable<Product> GetProduct(int? categoryId, string sortBy, bool? desc, bool? activeFilter, string search = "")
        {
            IQueryable<Product> data = _repo.All().AsQueryable();
            
            if (categoryId.HasValue)
            {
                data = data.Where(p => p.CategoryId == categoryId);
            }

            if (string.IsNullOrEmpty(search) == false)
            {
                data = data.Where(p => p.ProductName.Contains(search));
            }

            if (activeFilter.HasValue)
            {
                data = data.Where(p => p.Active == activeFilter);
            }

            data = data.OrderBy(p => p.Price);

            switch (sortBy)
            {
                case "price":
                    if (desc.HasValue && desc.Value)
                    {
                        data = data.OrderByDescending(p => p.Price);
                    }
                    else
                    {
                        data = data.OrderBy(p => p.Price);
                    }
                    break;
                default:
                    if (desc.HasValue && desc.Value)
                    {
                        data = data.OrderByDescending(p => p.ProductId);
                    }else
                    {
                        data = data.OrderBy(p => p.ProductId);
                    }
                    break;
            }

            return data;
        }

        public List<Product> GetProductByCategory(int categoryId)
        {
            return _repo.All().Where(p => p.CategoryId.Equals(categoryId)).ToList();
        }

        public Product GetProduct(int id)
        {
            return _repo.All().Where(p => p.ProductId == id).FirstOrDefault();
        }

        public void Add(Product product)
        {
            _repo.Add(product);
            this.Save();
        }

        public void Save()
        {
            _repo.UnitOfWork.Commit();
        }

        public void Delete(Product product)
        {
            //_repo.UnitOfWork.LazyLoadingEnabled = true;
            //_repo_Comment.UnitOfWork.LazyLoadingEnabled = true;

            try
            {
                List<Comment> Comments = product.Comment.ToList();

                foreach (var item in Comments)
                {
                    _repo_Comment.Delete(item);
                }

                _repo_Comment.UnitOfWork.Commit();

                _repo.Delete(product);
                this.Save();
            }
            finally
            {
                //_repo_Comment.UnitOfWork.LazyLoadingEnabled = false;
                //_repo.UnitOfWork.LazyLoadingEnabled = false;
            }
        }
    }
}