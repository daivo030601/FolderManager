using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Application.Common.Behaviors
{
    //logs the responses' elapsed time in milliseconds of
    internal class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;
        public PerformanceBehavior(ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //calculate time to excute the request
            _timer.Start();
            //RequestHandlerDelegate next, which calls the next thing in the pipeline.
            var response = await next();
            _timer.Stop();

            //check if time to excute request over 500 millisecond then dont return the response
            var elapsedMilliseconds = _timer.ElapsedMilliseconds;
            if (elapsedMilliseconds <= 500)
                return response;

            var requestName = typeof(TRequest).Name;    

            // log performance
            _logger.LogWarning("Travel Long Running Request: {Name} ({ElapsedMilleseconds} milliseconds) {@Request}",
                requestName, elapsedMilliseconds, request);
            return response;
        }

        
    }
}
