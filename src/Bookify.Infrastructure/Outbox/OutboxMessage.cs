using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Outbox
{
    public sealed class OutboxMessage
    {
        public Guid Id { get; private set; }
        public DateTime OccuredOnUtc { get; private set; }
        public string Type { get; private set; }
        public string Content { get; private set; }
        public DateTime? ProcessedOnUtc { get; private set; }
        public string? Error { get; private set; }

        public OutboxMessage(
            Guid id,
            DateTime occuredOnUtc,
            string type,
            string content)
        {
            Id = id;
            OccuredOnUtc = occuredOnUtc;
            Type = type;
            Content = content;
        }
    }
}