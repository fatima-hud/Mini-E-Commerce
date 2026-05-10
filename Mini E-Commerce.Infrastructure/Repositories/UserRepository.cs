using Mini_E_Commerce.Core.Models;
using Mini_E_Commerce.Infrastructure.Abstracts;
using Mini_E_Commerce.Infrastructure.Context;
using MiniECommerce.Infrastructure.InfrastructureBases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Infrastructure.Repositories
{
    public class UserRepository : GenericRepositoryAsync<UserModel>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
