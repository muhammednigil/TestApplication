using System.Collections.Generic;

namespace TestApplicationDomain.DTO.Response
{
    public class HotelDetailResponse : HotelResponse
    {
        public IList<HotelContactDto> HotelContactList { get; set; }
        public IList<RoomDto> RoomDetails { get; set; }
        public IList<RatingDto> Ratings { get; set; }
        public IList<FacilityDto> HotelFacilities { get; set; }
        public IList<HotelImageDto> HotelImages { get; set; }
    }

}


