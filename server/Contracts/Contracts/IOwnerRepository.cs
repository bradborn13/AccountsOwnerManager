using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IOwnerRepository: IRepositoryBase<Owner>
    {
        public Task<IEnumerable<Owner>> GetAllOwners();
        public Task<Owner> GetOwnerById(int ownerId);
        public Task<Owner> GetOwnerWithDetails(int ownerId);
        public void CreateOwner(Owner owner);
        public void UpdateOwner(Owner owner);
        public void DeleteOwner(Owner owner);
    }
}
