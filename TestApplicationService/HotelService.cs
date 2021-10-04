using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApplicationDomain;
using TestApplicationDomain.DTO;
using TestApplicationDomain.DTO.Request;
using TestApplicationDomain.DTO.Response;
using TestApplicationDomain.DynamicQuery;
using TestApplicationDomain.Entities;
using TestApplicationDomain.Validators;
using TestApplicationInterface.Repository;
using TestApplicationInterface.Service;
using TestApplicationService.ServiceUtilities;

namespace TestApplicationService
{
    public class HotelService : IHotelService
    {
        private readonly IMapper _mapper;
        private readonly IHotelRepository _hotelRepository;
        private readonly IPricingRepository _pricingRepository;
        private readonly IPricingDiscountRepository _pricingDiscountRepository;
        private readonly IPricingUserDiscountRepository _pricingUserDiscountRepository;
        private readonly IHotelContactRepository _hotelContactRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IHotelImageRepository _hotelImageRepository;

        public HotelService(IMapper mapper, 
            IHotelRepository hotelRepository,
            IPricingRepository pricingRepository,
            IPricingDiscountRepository pricingDiscountRepository,
            IPricingUserDiscountRepository pricingUserDiscountRepository,
            IHotelContactRepository hotelContactRepository,
            IRatingRepository ratingRepository,
            IFacilityRepository facilityRepository,
            IRoomRepository roomRepository,
            IHotelImageRepository hotelImageRepository)
        {
            _mapper = mapper;
            _hotelRepository = hotelRepository;
            _pricingRepository = pricingRepository;
            _pricingDiscountRepository = pricingDiscountRepository;
            _pricingUserDiscountRepository = pricingUserDiscountRepository;
            _hotelContactRepository = hotelContactRepository;
            _ratingRepository = ratingRepository;
            _facilityRepository = facilityRepository;
            _roomRepository = roomRepository;
            _hotelImageRepository = hotelImageRepository;
        }

        public async Task<IPagedData<Hotel>> GetHotels(HotelRequest request, DataDescriptor dataDescriptor)
        {
            return (await _hotelRepository.GetAllAsync(
                e => ServiceUtilities.Extensions.GetDistance(request.Longitude, request.Latitude, e.Longitude, e.Latitude) <= 15, //15 is the default diameter in range 
                x => x.OrderByDescending(item => item.FinalRating), null, null, null))
                .Map<IEnumerable<Hotel>>()
                .AsQueryable()
                .ApplyDescriptorWithPagination(dataDescriptor);

        }

        public async Task<IPagedData<HotelResponse>> GetHotelResponse(HotelRequest request, DataDescriptor dataDescriptor)
        {
            var hotels = _mapper.Map<IPagedData<HotelResponse>>(await GetHotels(request, dataDescriptor));

            foreach (var hotel in hotels.Data)
            {
                request.HotelId = hotel.ID;
                hotel.HowFar = ServiceUtilities.Extensions.GetDistance(request.Longitude, request.Latitude, hotel.Longitude, hotel.Latitude);
                var result = await CalculateBestPricingPerHotel(request);
                hotel.BasePrice = result.BasePrice;
                hotel.Discount = result.Discount;
                hotel.UserDiscount = result.UserDiscount;
            }
            return hotels;
        }

        public async Task<HotelDetailResponse> GetHotelDeatils(HotelRequest request)
        {
            var response = _mapper.Map<HotelDetailResponse>(await _hotelRepository.GetById(request.HotelId));
            response.RoomDetails = _mapper.Map<List<RoomDto>>((await _roomRepository.GetHotelRooms(response.ID)).ToList());
            response.Ratings = _mapper.Map<List<RatingDto>>((await _ratingRepository.GetHotelRating(response.ID)).ToList());
            response.HotelFacilities = _mapper.Map<List<FacilityDto>>((await _facilityRepository.GetHotelFacility(response.ID)).ToList());
            response.HotelImages = _mapper.Map<List<HotelImageDto>>((await _hotelImageRepository.GetHotelImages(response.ID)).ToList());
            var result = await CalculateBestPricingPerHotel(request);
            response.BasePrice = result.BasePrice;
            response.Discount = result.Discount;
            response.UserDiscount = result.UserDiscount;

            return response;
        }
        
        private async Task<HotelResponse> CalculateBestPricingPerHotel(HotelRequest request)
        {
            var price = await GetLowestPricing(request.HotelId);
            request.RoomId = price.Room.Id;

            var pricingDiscount = await GetHighestPricingDiscount(request);

            var pricingUserDiscount = await GetHighestUserDiscount(request);

            return new HotelResponse
            {
                BasePrice = price.BasePrice,
                Discount = pricingDiscount.DiscountType.Equals("P") ? price.BasePrice * (pricingDiscount.Discount / 100) : price.BasePrice - pricingDiscount.Discount,
                UserDiscount = string.IsNullOrWhiteSpace(pricingUserDiscount.DiscountType) ? 0 : pricingUserDiscount.DiscountType.Equals("P") ? price.BasePrice * (pricingDiscount.Discount / 100) : price.BasePrice - pricingDiscount.Discount
            };
        }

        private Task<Pricing> GetLowestPricing(string hotelId)
        {
            return Task.Run(() => _pricingRepository.Get(x => x.Room.Hotel.Id == hotelId).OrderBy(o => o.BasePrice).FirstOrDefault());
        }

        private Task<PricingDiscount> GetHighestPricingDiscount(HotelRequest request)
        {
            var result = _pricingDiscountRepository.Get(x => x.Room.Id == request.RoomId);

            if (request.CheckInDate.HasValue)
                request.CheckInDate = DateTime.Now;
            result = result.Where(w => w.StartDate <= request.CheckInDate);

            if (request.CheckOutDate.HasValue)
                result = result.Where(w => w.ExpiryDate >= request.CheckOutDate);

            return Task.Run(() => result.OrderBy(o => o.StartDate).FirstOrDefault());
        }

        private Task<PricingUserDiscount> GetHighestUserDiscount(HotelRequest request)
        {
            if((new UserValidator().Validate(new UserDto { Email = request.Email })).IsValid)
            {
                var result = _pricingUserDiscountRepository.Get(x => x.Room.Id == request.RoomId && x.User.Email == request.Email);

                if (request.CheckInDate.HasValue)
                    request.CheckInDate = DateTime.Now;
                result = result.Where(w => w.StartDate <= request.CheckInDate);

                if (request.CheckOutDate.HasValue)
                    result = result.Where(w => w.ExpiryDate >= request.CheckOutDate);

                return Task.Run(() => result.OrderBy(o => o.StartDate).FirstOrDefault());
            }
            return Task.Run(() => new PricingUserDiscount());
        }
    }
}
