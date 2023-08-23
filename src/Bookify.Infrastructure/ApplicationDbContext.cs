using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Application.Abstractions.Clock;
using Bookify.Application.Exceptions;
using Bookify.Domain.Abstractions;
using Bookify.Infrastructure.Clock;
using Bookify.Infrastructure.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Bookify.Infrastructure
{
    public sealed class ApplicationDbContext : DbContext, IUnitOfWork
    {
        private readonly IPublisher _publisher;
        private readonly IDateTimeProvider _dateTimeProvider;

        private static readonly JsonSerializerSettings _jsonSerializerSettings = new()
        {
            TypeNameHandling = TypeNameHandling.All
        };

        public ApplicationDbContext(DbContextOptions options, IPublisher publisher, IDateTimeProvider dateTimeProvider)
            : base(options)
        {
            _publisher = publisher;
            _dateTimeProvider = dateTimeProvider;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                await PublishDomainEventsAsync();

                var result = await base.SaveChangesAsync(cancellationToken);

                return result;
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyException("Concurrency exception occured", e);
            }
        }

        private async Task PublishDomainEventsAsync()
        {
            var domainEvents = ChangeTracker
                .Entries<Entity>()
                .Select(entry => entry.Entity)
                .SelectMany(entity =>
                {
                    var domainEvents = entity.GetDomainEvents();

                    entity.ClearDomainEvents();

                    return domainEvents;
                })
                .Select(e => new OutboxMessage(
                    Guid.NewGuid(),
                    _dateTimeProvider.UtcNow,
                    e.GetType().Name,
                    JsonConvert.SerializeObject(e, _jsonSerializerSettings)))
                .ToList();

            AddRange(domainEvents);
        }
    }
}