﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookify.Infrastructure.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName)
                .HasMaxLength(200)
                .HasConversion(fn => fn.Value, value => new FirstName(value));

            builder.Property(u => u.LastName)
                .HasMaxLength(200)
                .HasConversion(ln => ln.Value, value => new LastName(value));

            builder.Property(u => u.Email)
                .HasMaxLength(200)
                .HasConversion(e => e.Value, value => new Domain.Users.Email(value));

            builder.HasIndex(u => u.Email).IsUnique();
        }
    }
}