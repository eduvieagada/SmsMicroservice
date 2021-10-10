using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmsMicroservice.Core;
using SmsMicroservice.Core.Messaging;
using SmsMicroservice.Core.Notifications;
using SmsMicroservice.Core.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmsMicroservice
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly SmsService _smsService;

        public Worker(ILogger<Worker> logger, SmsService smsService)
        {
            _logger = logger;
            _smsService = smsService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            _smsService.InitializeService();

            await Task.CompletedTask;
        }
    }
}
