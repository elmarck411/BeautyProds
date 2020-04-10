using AutoMapper;
using BeautyProds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyProds.Profiles
{
    public class BottleProfile :Profile
    {
        public BottleProfile()
        {
            CreateMap<_Bottle,Bottle>().ReverseMap();
        }
    }
}