﻿using AutoMapper;
using WebDienTu.Data;
using WebDienTu.ViewModels;

namespace WebDienTu.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterVM, KhachHang>();
            //.ForMember(kh => kh.HoTen, option => option.MapFrom(RegisterVM => RegisterVM.HoTen))
            //.ReverseMap();
        }
    }
}
