using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TestApplicationDomain.DTO.Response;
using TestApplicationDomain.Entities;

namespace TestApplicationDomain.AutoMapper
{
    public class HotelMappingProfile : Profile
    {
        public HotelMappingProfile()
        {
            CreateMap<Hotel, HotelResponse>();
            CreateMap<Hotel, HotelDetailResponse>();
            CreateMap<IPagedData<Hotel>, IPagedData<HotelResponse>>();
        }
    }
}
