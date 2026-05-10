using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Core.Models
{
    public class UserModel:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string Email { get; set; }
        public string Role { get; set; }
        public CartModel Cart { get; set; }
        public ICollection<OrderModel> Orders { get; set; }
    }
}
