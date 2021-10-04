using AutoMapper;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplicationDomain.DTO;
using TestApplicationDomain.Entities;
using TestApplicationDomain.Validators;
using TestApplicationInterface;
using TestApplicationInterface.Repository;
using TestApplicationInterface.Service;

namespace TestApplicationService
{
    public class BookingService : IBookingService
    {
        private readonly IMapper _mapper;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IMapper mapper,
           IBookingRepository bookingRepository,
           IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> SaveBooking(BookingDto booking)
        {
            var validate = await new BookingValidator().ValidateAsync(booking);

            if (!validate.IsValid)
                throw new ValidationException();

            var bookingRequest = _mapper.Map<Booking>(booking);
            bookingRequest.CreatedBy = "user";
            bookingRequest.CreatedDate = DateTime.Now;

            _bookingRepository.Add(bookingRequest);

            var rows = await _unitOfWork.CommitAsync();
            if (rows > 0) return true;
            return false;
        }
    }
}
