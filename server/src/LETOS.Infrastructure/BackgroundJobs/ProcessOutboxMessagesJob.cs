using LETOS.Share.Abstractions.Message;
using LETOS.Persistence;
using LETOS.Persistence.Outbox;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

namespace LETOS.Infrastructure.BackgroundJobs;


[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private static readonly JsonSerializerSettings JsonSerializerSettings = new ()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    private readonly ApplicationDbContext _dbContext;
    private readonly IPublishEndpoint _publishEndpoint; // Maybe can use more Rebus library
    private readonly ILogger<ProcessOutboxMessagesJob> _logger;

    public ProcessOutboxMessagesJob(ApplicationDbContext dbContext, IPublishEndpoint publishEndpoint, ILogger<ProcessOutboxMessagesJob> logger)
    {
        _dbContext = dbContext;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        IReadOnlyList<OutboxMessage> messages = await _dbContext
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            .OrderBy(m => m.OccurredOnUtc)
            .Take(20)
            .ToListAsync(context.CancellationToken);

        foreach (OutboxMessage outboxMessage in messages)
        {
            Exception? exception = null;

            try
            {
                IDomainEvent domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                    outboxMessage.Content,
                    JsonSerializerSettings) !;

                await _publishEndpoint.Publish(domainEvent, context.CancellationToken);
            }
            catch (Exception caughtException)
            {
                _logger.LogError(
                    caughtException,
                    "Exception while processing outbox message {MessageId}",
                    outboxMessage.Id);

                exception = caughtException;
            }

            outboxMessage.Error = exception?.ToString();
            outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
        }

        await _dbContext.SaveChangesAsync();
    }
}
