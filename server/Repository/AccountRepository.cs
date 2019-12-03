using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    class AccountRepository:RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
          
        }
        public async Task<IEnumerable<Account>> AccountsByOwner(int ownerId)
        {
            return await FindByCondition(x => x.OwnerId.Equals(ownerId)).ToListAsync();
        }
    }
}
