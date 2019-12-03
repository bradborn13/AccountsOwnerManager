using AutoMapper;
using Entities.Models;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Owner, OwnerDTO>();
            CreateMap<Account, AccountDTO>();
            CreateMap<OwnerForCreateDTO, Owner>();
            CreateMap<OwnerForUpdateDTO, Owner>();
        }
    }
}
