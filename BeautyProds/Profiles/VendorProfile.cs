using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BeautyProds.Models;

namespace BeautyProds.Profiles
{
    public class VendorProfile : Profile
    {
        public VendorProfile()
        {
            CreateMap<Vendor, _Vendor>().ReverseMap();
           
        }
    }
}