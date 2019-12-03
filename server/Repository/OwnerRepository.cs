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
    class OwnerRepository:RepositoryBase<Owner>, IOwnerRepository
    {
        public OwnerRepository(RepositoryContext repositoryContext):base(repositoryContext)
        {

        }
        public async Task<IEnumerable<Owner>> GetAllOwners()
        {
            return await FindAll().OrderBy(o => o.Name).ToListAsync();

        }
        public async Task<Owner> GetOwnerById(int ownerId)
        {
            return await FindByCondition(x => x.id.Equals( ownerId)).FirstOrDefaultAsync();
        }
        public async Task<Owner> GetOwnerWithDetails(int ownerId) 
        {
            return await FindByCondition(x => x.id.Equals(ownerId)).Include(ox =>ox.Accounts).FirstOrDefaultAsync();
        }
        public void CreateOwner(Owner owner)
        {
            Create(owner);
        }
        public void UpdateOwner(Owner owner)
        {
            Update(owner);
        }
         public void DeleteOwner(Owner owner)
        {
            Delete(owner);
        }
    }
}
