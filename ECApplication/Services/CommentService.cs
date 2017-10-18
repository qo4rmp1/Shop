using ECApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECApplication.Services
{
    public class CommentService
    {
        CommentRepository _repo = RepositoryHelper.GetCommentRepository();

        public CommentService()
        {
            _repo.UnitOfWork.LazyLoadingEnabled = false;
        }

        public List<Comment> GetCommentByProduct(int productId)
        {
            return _repo.All().Where(p => p.ProductId == productId).ToList();
        }

        public void Add(Comment comment)
        {
            _repo.Add(comment);
        }
        
        public Comment Find(int id, string username)
        {
            return _repo.All().Where(p => p.Id == id && p.UserName == username).FirstOrDefault();
        }

        public void Delete(Comment comment)
        {
            _repo.Delete(comment);
        }

        public void Save()
        {
            _repo.UnitOfWork.Commit();
        }

        public bool AlreadyCreate(int productId, string userName)
        {
            return _repo.All().Any(p => p.ProductId == productId && p.UserName == userName);
        }
    }
}