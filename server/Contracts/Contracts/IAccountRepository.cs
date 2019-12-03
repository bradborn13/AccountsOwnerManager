using Entities.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAccountRepository:IRepositoryBase<Account>
    {
        public  Task<IEnumerable<Account>> AccountsByOwner(int ownerId);
    }
}
