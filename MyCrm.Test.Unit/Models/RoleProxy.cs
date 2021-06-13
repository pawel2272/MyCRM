using System;
using MyCrm.Domain.Entities;

namespace MyCrm.Test.Unit.Models
{
    public class RoleProxy : Role
    {
        public RoleProxy(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }

        public void AddUser(User user)
        {
            this.Users.Add(user);
        }
    }
}
