using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Shared;

namespace Bookify.Domain.Apartments
{
    public sealed class Apartment : Entity
    {
        public Name Name { get; private set; }
        public Description Description { get; private set; }
        public string Country { get; private set; }
        public Address Address { get; private set; }
        public Money Price { get; private set; }
        public Money CleaningFee { get; private set; }
        public DateTime? LastBookedOnUtc { get; internal set; }
        public List<Amenity> Amenities { get; private set; } = new();

        private Apartment()
        { }

        public Apartment(Guid id,
            Name name,
            Description description,
            string country,
            Address address,
            Money price,
            Money cleaningFee,
            DateTime? lastBookedOnUtc,
            List<Amenity> amenities) :
            base(id)
        {
            Name = name;
            Description = description;
            Country = country;
            Address = address;
            Price = price;
            CleaningFee = cleaningFee;
            LastBookedOnUtc = lastBookedOnUtc;
            Amenities = amenities;
        }
    }
}