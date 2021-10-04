using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApplicationDomain;
using TestApplicationDomain.DTO.Request;
using TestApplicationDomain.DTO.Response;
using TestApplicationDomain.DynamicQuery;

namespace TestApplicationInterface.Service
{
    public interface IHotelService
    {
        Task<IPagedData<HotelResponse>> GetHotelResponse(HotelRequest request, DataDescriptor dataDescriptor);
        Task<HotelDetailResponse> GetHotelDeatils(HotelRequest request);
    }
}
