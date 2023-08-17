using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Application.Abstractions.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bookify.Application.Abstractions.Behaviors
{
    public class LoggingBehavior<TRequest, TRespone> :
        IPipelineBehavior<TRequest, TRespone>
        where TRequest : IBaseCommand
    {
        private readonly ILogger<TRequest> _logger;

        public LoggingBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TRespone> Handle(
            TRequest request,
            RequestHandlerDelegate<TRespone> next,
            CancellationToken cancellationToken)
        {
            var name = request.GetType().Name;

            try
            {
                _logger.LogInformation("Executing command {Command}", name);

                var result = await next();

                _logger.LogInformation("Command {Command} proccessed successfully", name);

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Command {Command} proccessing failed", name);
                throw;
            }
        }
    }
}