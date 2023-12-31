﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Apartments;
using Bookify.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookify.Infrastructure.Configurations
{
    internal sealed class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.ToTable("apartments");

            builder.HasKey(a => a.Id);

            builder.OwnsOne(a => a.Address);

            builder.Property(a => a.Name)
                .HasMaxLength(200)
                .HasConversion(n => n.Value, value => new Name(value));

            builder.Property(a => a.Description)
                .HasMaxLength(2000)
                .HasConversion(d => d.Value, value => new Description(value));

            builder.OwnsOne(
                a => a.Price,
                priceBuilder =>
                {
                    priceBuilder.Property(m => m.Currency)
                        .HasConversion(c => c.Code, code => Currency.FromCode(code));
                });

            builder.OwnsOne(
                a => a.CleaningFee,
                priceBuilder =>
                {
                    priceBuilder.Property(m => m.Currency)
                        .HasConversion(c => c.Code, code => Currency.FromCode(code));
                });

            builder.Property<uint>("Version").IsRowVersion();
        }
    }
}