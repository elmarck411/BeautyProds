﻿using AutoMapper;
using BeautyProds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyProds.Profiles
{
    public class BottleRequestProfile : Profile
    {
        public BottleRequestProfile()
        {
            CreateMap<BottleRequest, _BottleRequest>().ForMember(dest => dest.shortDate, m => m.Ignore()).ReverseMap();
        }
    }
}