using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Bookify.Application.Bookings.ReserveBooking
{
    public sealed class ReserveBookingCommandValidator : AbstractValidator<ReserveBookingCommand>
    {
        public ReserveBookingCommandValidator()
        {
            RuleFor(c => c.UserId).NotEmpty();

            RuleFor(x => x.ApartmentId).NotEmpty();

            RuleFor(x => x.StartDate).LessThan(c => c.EndDate);
        }
    }
}