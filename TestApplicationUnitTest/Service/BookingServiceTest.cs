using AutoMapper;
using FluentValidation;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApplicationDomain.DTO;
using TestApplicationDomain.Entities;
using TestApplicationInterface;
using TestApplicationInterface.Repository;
using TestApplicationInterface.Service;
using TestApplicationService;
using Xunit;

namespace TestApplicationUnitTest.Service
{
    public class BookingServiceTest
    {
        private readonly IMapper _mockMapper;
        private readonly Mock<IBookingRepository> _mockBookingRepo;
        private readonly Mock<IUnitOfWork> _mockUoW;
        private readonly IBookingService _bookingService;
        public BookingServiceTest()
        {
            _mockBookingRepo = new Mock<IBookingRepository>();
            _mockUoW = new Mock<IUnitOfWork>();

            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<BookingDto, Booking>().ReverseMap();
            });
            _mockMapper = config.CreateMapper();
            _bookingService = new BookingService(_mockMapper, _mockBookingRepo.Object, _mockUoW.Object);
        }

        [Fact]
        public async Task SaveBooking_ShouldReturnOkAsync()
        {
            _mockBookingRepo.Setup(x => x.Add(It.IsAny<Booking>()));

            _mockUoW.Setup(x => x.CommitAsync()).Returns(Task.FromResult(1));

            var res = _bookingService.SaveBooking(MockData.MockData.BookingRequestData(true));
        }

    }
}
