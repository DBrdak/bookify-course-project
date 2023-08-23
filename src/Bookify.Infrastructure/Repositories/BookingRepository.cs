using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Repositories
{
    internal sealed class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private static readonly BookingStatus[] _activeBookingStatuses =
        {
            BookingStatus.Confirmed,
            BookingStatus.Reserverd,
            BookingStatus.Completed
        };

        public BookingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> IsOverlappingAsync(
            Apartment apartment,
            DateRange duration,
            CancellationToken cancellationToken = default)
        {
            return await _context
                .Set<Booking>()
                .AnyAsync(
                    b =>
                        b.ApartmentId == apartment.Id &&
                        b.Duration.Start <= duration.End &&
                        b.Duration.End >= duration.Start &&
                        _activeBookingStatuses.Contains(b.Status),
                    cancellationToken);
        }
    }
}