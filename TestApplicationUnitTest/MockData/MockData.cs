using System;
using System.Collections.Generic;
using System.Text;
using TestApplicationDomain.DTO;

namespace TestApplicationUnitTest.MockData
{
    public static class MockData
    {
        public static BookingDto BookingRequestData(bool isCorrect)
        {
            if (isCorrect)
            {
                return new BookingDto
                {
                    AdultGuestCOunt = 2,
                    InfantGuestCOunt = 1,
                    KidsGuestCOunt = 1,
                    Room = new RoomDto
                    {
                        Code = "sgsgsgs",
                        Hotel = new HotelDto
                        {
                            ID = "sss",
                            Name = "test"
                        },
                        ID = "dkdkdkd",
                        RoomLimit = 2
                    },
                    Price = 10000,
                    DiscountedPrice = 100,
                    VATPrice = 120000,
                    Start = DateTime.Now,
                    End = DateTime.Now.AddDays(10),
                    TotalPrice = 122222,
                    User = new UserDto
                    {
                        Email = "sjjsj@jsjs.sss"
                    }
                };
            }
            else
            {
                return new BookingDto
                {
                    AdultGuestCOunt = 0,
                    InfantGuestCOunt = 1,
                    KidsGuestCOunt = 1,
                    DiscountedPrice = 100,
                    VATPrice = 120000,
                    Start = DateTime.Now,
                    End = DateTime.Now.AddDays(10),
                    TotalPrice = 122222,
                    User = new UserDto
                    {
                        Email = "sjjsj@jsjs.sss"
                    }
                };
            }
        }
    }
}
