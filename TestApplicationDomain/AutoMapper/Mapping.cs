using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TestApplicationDomain.DTO;
using TestApplicationDomain.Entities;

namespace TestApplicationDomain.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<BookingDto, Booking>().ReverseMap();
            CreateMap<CityDto, City>().ReverseMap();
            CreateMap<CountryDto, Country>().ReverseMap();
            CreateMap<FacilityDto, Facility>().ReverseMap();
            CreateMap<HotelContactDto, HotelContact>().ReverseMap();
            CreateMap<HotelDto, Hotel>().ReverseMap();
            CreateMap<PricingDiscountDto, PricingDiscount>().ReverseMap();
            CreateMap<PricingDto, Pricing>().ReverseMap();
            CreateMap<PricingUserDiscountDto, PricingUserDiscount>().ReverseMap();
            CreateMap<RatingDto, Rating>().ReverseMap();
            CreateMap<RoomDto, Room>().ReverseMap();
            CreateMap<RoomFacilityDto, RoomFacility>().ReverseMap();
            CreateMap<RoomFacilityTypeDto, RoomFacilityType>().ReverseMap();
            CreateMap<RoomTypeDto, RoomType>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<HotelImageDto, HotelImage>().ReverseMap();
        }
    }
}
