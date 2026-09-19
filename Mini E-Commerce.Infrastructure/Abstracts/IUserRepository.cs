using MiniECommerce.Core.Models;
using MiniECommerce.Infrastructure.InfrastructureBases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniECommerce.Infrastructure.Abstracts
{
    public interface IUserRepository:IGenericRepositoryAsync<UserModel>
    {
    }
}
