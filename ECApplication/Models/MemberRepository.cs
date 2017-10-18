using System;
using System.Linq;
using System.Collections.Generic;
	
namespace ECApplication.Models
{
    public class MemberRepository : EFRepository<Member>, IMemberRepository
    {
        public Member Find(string username)
        {
            return base.All().Where(p => p.UserName.Equals(username)).FirstOrDefault();
        }
    }

    public  interface IMemberRepository : IRepository<Member>
	{

	}
}