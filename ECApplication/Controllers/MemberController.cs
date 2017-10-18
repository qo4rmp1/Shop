using ECApplication.Models;
using ECApplication.Models.ViewModels;
using ECApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ECApplication.Controllers
{
    public class MemberController : Controller
    {
        MemberService _memberService = new MemberService();
        private MailService _mailService = new MailService();

        // 會員註冊
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Photo");
            else
                return View();
        }

        [HttpPost]
        public ActionResult Register(MemberRegisterVM memberRegister)
        {
            if (ModelState.IsValid)
            {
                Member Member = new Member();
                ModelMapping<MemberRegisterVM, Member> VMM = new ModelMapping<MemberRegisterVM, Member>();
                VMM.Mapping(memberRegister, Member);

                //Member.AuthCode = new Guid().ToString();
                _memberService.Register(Member);
                UriBuilder ValidateUrl = new UriBuilder(Request.Url)
                {
                    Path = Url.Action("EmailValidate", "Member", new { userName = Member.UserName, AuthCode = Member.AuthCode })
                };

                string TempMail = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/AccountRegisterEMailTemplate.htm"));
                string MailBody = _mailService.GetRegisterMailBody(
                    TempMail,
                    Member.UserName,
                    ValidateUrl.ToString().Replace("%3F", "?")
                );

                _mailService.SendRegisterMail(MailBody, Member.Email);
                TempData["RegisterState"] = "註冊成功，請至信箱驗證信件";
                return RedirectToAction("RegisterResult");

                //RegisterMember.newMember.Password = RegisterMember.Password;
                //RegisterMember.newMember.AuthCode = "1";  //new Guid().ToString();
                //MemberService.Register(RegisterMember.newMember);
                //UriBuilder ValidateUrl = new UriBuilder(Request.Url)
                //{
                //    Path = Url.Action("EmailValidate", "Member", new { UserName = RegisterMember.newMember.Member, AuthCode = RegisterMember.newMember.AuthCode })
                //};

                //string TempMail = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MemberRegisterEMailTemplate.htm"));
                //string MailBody = MailService.GetRegisterMailBody(
                //    TempMail,
                //    RegisterMember.newMember.Member,
                //    ValidateUrl.ToString().Replace("%3F", "?")
                //);

                //MailService.SendRegisterMail(MailBody, RegisterMember.newMember.Email);
            }

            memberRegister.Password = null;
            memberRegister.PasswordCheck = null;

            return View(memberRegister);
        }

        public ActionResult RegisterResult()
        {
            return View();
        }

        public ActionResult AccountCheck(MemberRegisterVM memberRegister)
        {
            return Json(_memberService.AccountCheck(memberRegister.UserName), JsonRequestBehavior.AllowGet);
            //return Json(MemberService.AccountCheck(Member.newMember.Account), JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmailValidate(string username, string authCode)
        {
            ViewData["EmailValidate"] = _memberService.EmailValidate(username, authCode);
            return View();
        }

        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (string.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Redirect(returnUrl);
                }
            }
            else
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(MemberLoginVM member, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                string ValidateStr = _memberService.LoginCheck(member.Username, member.Password);

                if (string.IsNullOrEmpty(ValidateStr))
                {
                    string RoleData = _memberService.GetRole(member.Username);

                    FormsAuthenticationTicket Ticket = new FormsAuthenticationTicket(
                        version: 1,
                        name: member.Username,
                        issueDate: DateTime.Now,
                        expiration: DateTime.Now.AddMinutes(30),
                        isPersistent: false,
                        userData: RoleData,
                        cookiePath: FormsAuthentication.FormsCookiePath);

                    string enTicket = FormsAuthentication.Encrypt(Ticket);
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, enTicket));

                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", ValidateStr);
                    member.Password = null;
                    return View(member);
                }
            }
            else
            {
                //debug
                ModelState.AddModelError("", "模型驗證不正確");
                member.Password = null;
                return View(member);
            }
        }
        
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        //[Authorize]
        public ActionResult ChangePassword()
        {
            ChangePasswordVM ChangePassword = new ChangePasswordVM();
            return View(ChangePassword);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordVM changePassword)
        {
            if (ModelState.IsValid)
            {
                ViewData["ChangeState"] = _memberService.ChangePassword(User.Identity.Name, changePassword.Password, changePassword.NewPassword);
            }
            return View();
        }
    }
}