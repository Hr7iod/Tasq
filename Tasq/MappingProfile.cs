﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;

namespace Tasq
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Entities.Models.Tasq, TasqDto>();

            CreateMap<TasqForCreationDto, Entities.Models.Tasq>();

            CreateMap<TasqForUpdateDto, Entities.Models.Tasq>().ReverseMap();

            CreateMap<UserForRegistrationDto, User>();

        }
    }
}
