using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Apartments;
using Bookify.Domain.Shared;

namespace Bookify.Domain.Bookings
{
    public sealed record PricingDetails(
        Money PriceForPeriod,
        Money CleaningFee,
        Money AmenitiesUpCharge,
        Money TotalPrice);
}