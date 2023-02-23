using Icarus.Domain.Events;
using Icarus.Persistence;
using Icarus.Persistence.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Quartz;

namespace Icarus.Infrastructure.BackgroundJobs;
public class ProcessOutboxMessagesJob : IJob
{
    private readonly IcarusDbContext _context;
    private readonly IPublisher _publisher;
    private readonly ILogger<ProcessOutboxMessagesJob> _logger;

    public ProcessOutboxMessagesJob(IcarusDbContext context, IPublisher publisher, ILogger<ProcessOutboxMessagesJob> logger)
    {
        _context = context;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            List<OutboxMessage> messages = await _context.OutboxMessages
           .Where(x => x.ProcessOnUtc == null)
           .Take(20)
           .ToListAsync(context.CancellationToken);

            foreach (OutboxMessage message in messages)
            {
                IDomainEvent? domainEvent = JsonConvert
                    .DeserializeObject<IDomainEvent>(message.Content,
                    new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

                if (domainEvent is null)
                {
                    _logger.LogInformation("DomainEvent is null");
                    continue;
                }

                try
                {
                    await _publisher.Publish(domainEvent);
                    message.ProcessOnUtc = DateTime.UtcNow;
                }
                catch (Exception ex)
                {
                    message.Error = ex.ToString();
                    message.ProcessOnUtc = DateTime.UtcNow;
                }


                //Polly.Retry.RetryPolicy policy = Policy
                //    .Handle<Exception>()
                //    .WaitAndRetryAsync(3, attemp => TimeSpan.FromSeconds(attemp * 30));

                //PolicyResult result = await policy
                //    .ExecuteAndCaptureAsync(() => 
                //    _publisher.Publish(domainEvent));

                //message.Error = result.FinalException?.ToString();
                //message.ProcessOnUtc = DateTime.UtcNow;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }

        await _context.SaveChangesAsync();
    }
}