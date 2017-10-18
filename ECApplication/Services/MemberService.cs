using ECApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ECApplication.Services
{
    public class MemberService
    {
        MemberRepository _repo = RepositoryHelper.GetMemberRepository();

        public string Login(string username, string password)
        {
            Member Member = this.GetMember(username);

            if (Member != null)
            {
                if (string.IsNullOrWhiteSpace(Member.AuthCode))
                {
                    if (this.PasswordCheck(Member, password))
                    {
                        return "";
                    }
                    else
                    {
                        return "密碼錯誤";
                    }
                }
                else
                {
                    return "登入帳號尚未驗證成功";
                }
            }
            else
            {
                return "查無此使用者";
            }
        }

        public void Register(Member member)
        {
            member.SetHashPassword();
            _repo.Add(member);
            this.Save();
        }

        public string EmailValidate(string username, string authCode)
        {
            string ValidateStr = string.Empty;

            Member Member = _repo.Find(username);

            if (null != Member)
            {
                if (Member.AuthCode.Equals(authCode))
                {
                    Member.AuthCode = string.Empty;
                    this.Save();
                    ValidateStr = "帳號信箱驗證成功";
                }
                else
                {
                    ValidateStr = "驗證碼錯誤，請重新確認";
                }
            }
            else
            {
                ValidateStr = "找不到該會員資料，請重新確認";
            }
            return ValidateStr;
        }

        public bool AccountCheck(string username)
        {
            return (null != _repo.Find(username));
        }

        public string LoginCheck(string username, string password)
        {
            Member Member = _repo.Find(username);

            if (Member != null)
            {
                if (string.IsNullOrWhiteSpace(Member.AuthCode))
                {
                    if (this.PasswordCheck(Member, password))
                    {
                        return "";
                    }
                    else
                    {
                        return "密碼錯誤";
                    }
                }
                else
                {
                    return "登入帳號尚未驗證成功";
                }
            }
            else
            {
                return "查無此使用者";
            }
        }

        private bool PasswordCheck(Member member, string password)
        {
            return member.Password.Equals(FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1"));
        }

        public string GetRole(string username)
        {
            string Role = "User";
            Member member = _repo.Find(username);

            if (member != null)
            {
                if (member.IsAdmin)
                {
                    Role += ",Admin";
                }
            }

            return Role;
        }

        public string ChangePassword(string username, string password, string newPassword)
        {
            string msg = string.Empty;
            Member member = _repo.Find(username);

            if (member != null)
            {
                if (PasswordCheck(member, password))
                {
                    member.Password = newPassword;
                    member.SetHashPassword();
                    this.Save();
                    msg = "修改密碼成功";
                }
                else
                {
                    msg = "舊密碼輸入錯誤";
                }
            }
            return msg;
        }

        public void Save()
        {
            _repo.UnitOfWork.Commit();
        }

        public Member GetMember(string username)
        {
            return _repo.Find(username);
        }
    }
}