using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECApplication.Models;
using ECApplication.Services;

namespace ECApplication.Controllers
{
    public class CommentsController : Controller
    {
        CommentService _service = new CommentService();
        OrderHeaderService _service_OrderHeader = new OrderHeaderService();

        // GET: Comments
        public ActionResult Index(int productId)
        {
            var comment = _service.GetCommentByProduct(productId);
            return PartialView(comment.ToList());
        }
        
        // GET: Comments/Create
        public ActionResult Create(int productId)
        {
            ViewBag.ProductId = productId;
            return PartialView();
        }

        // POST: Comments/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelStateExclude("CreateTime,UserName")]
        public ActionResult Create([Bind(Include = "Content,ProductId,Star")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    comment.UserName = User.Identity.Name;
                    comment.CreateTime = DateTime.Now;
                    _service.Add(comment);
                    _service.Save();
                    return RedirectToAction("Detail", "Products", new {  id = comment.ProductId });
                }
                else
                {
                    return JavaScript("alert('請先登入');");
                }
            }

            return JavaScript("alert(\"模型驗證錯誤\");");
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = _service.Find(id.Value, User.Identity.Name);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelStateExclude("CreateTime,UserName")]
        public ActionResult Edit([Bind(Include = "Id,Content,ProductId,Star")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.CreateTime = DateTime.Now;
                comment.UserName = User.Identity.Name;
                _service.Save();
                return RedirectToAction("Detail", "Products", new { id = comment.ProductId });
            }
            return JavaScript("alert(\"模型驗證錯誤\");");
        }

        // POST: Comments/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Comment Comment = _service.Find(id, User.Identity.Name);

                if (Comment != null)
                {
                    int ProductId = Comment.ProductId.Value;
                    _service.Delete(Comment);
                    _service.Save();

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
            }
        }
    }
}
