using Icarus.Domain.Events;
using Icarus.Persistence;
using Icarus.Persistence.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using System.Diagnostics;

namespace Icarus.Infrastructure.BackgroundJobs;
public class ProcessOutboxMessagesJob : IJob
{
    private readonly IcarusDbContext _context;
    private readonly IPublisher _publisher;

    public ProcessOutboxMessagesJob(IcarusDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
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
                    Debug.WriteLine("DomainEvent is null");
                    continue;
                }

                await _publisher.Publish(domainEvent);

                message.ProcessOnUtc = DateTime.UtcNow;
            }
        }
        catch (Exception ex)
        {
            Debug.Write(ex.Message);
        }

        await _context.SaveChangesAsync();
    }
}