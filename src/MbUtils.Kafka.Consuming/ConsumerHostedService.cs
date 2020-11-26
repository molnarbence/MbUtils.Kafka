using Confluent.Kafka;
using MbUtils.Kafka.Consuming.Configs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MbUtils.Kafka.Consuming
{
   internal class ConsumerHostedService<TMessage> : BackgroundService
   {
      private readonly ConsumerConfig _consumerConfig;
      private readonly string _topic;
      private readonly ILogger<ConsumerHostedService<TMessage>> _logger;
      private readonly IServiceProvider _serviceProvider;
      private readonly int _delayMs;

      public ConsumerHostedService(
         IOptions<KafkaConsumerConfig> kafkaConsumerConfig, 
         ILogger<ConsumerHostedService<TMessage>> logger,
         IServiceProvider serviceProvider)
      {
         if (kafkaConsumerConfig.Value == null)
            throw new ArgumentNullException(nameof(kafkaConsumerConfig));
         var val = kafkaConsumerConfig.Value;

         if (string.IsNullOrEmpty(val.GroupId))
            throw new ArgumentNullException(nameof(val.GroupId));
         if (string.IsNullOrEmpty(val.BootstrapServers))
            throw new ArgumentNullException(nameof(val.BootstrapServers));
         if (string.IsNullOrEmpty(val.Topic))
            throw new ArgumentNullException(nameof(val.Topic));

         if (!Enum.TryParse(val.AutoOffsetReset, out AutoOffsetReset resetType))
            resetType = AutoOffsetReset.Latest;

         _consumerConfig = new ConsumerConfig
         {
            GroupId = val.GroupId,
            BootstrapServers = val.BootstrapServers,
            AutoOffsetReset = resetType
         };
         _topic = val.Topic;
         _logger = logger;
         _serviceProvider = serviceProvider;
         _delayMs = val.DelayMs;
      }
      protected override async Task ExecuteAsync(CancellationToken stoppingToken)
      {
         if (_delayMs > 0)
         {
            _logger.LogInformation("Delaying consuming by {0} milliseconds", _delayMs);
            await Task.Delay(_delayMs);
         }
         await ConsumerLoopAsync(stoppingToken);
      }

      private async Task ConsumerLoopAsync(CancellationToken stoppingToken)
      {
         using var c = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();

         _logger.LogInformation("Subscribing to topic {0}", _topic);
         c.Subscribe(_topic);
         _logger.LogInformation("Subscribed to topic {0}", _topic);

         stoppingToken.Register(() => _logger.LogDebug("{0} is stopping", nameof(ConsumerHostedService<TMessage>)));


         while (!stoppingToken.IsCancellationRequested)
         {
            try
            {
               await ConsumeAsync(c, stoppingToken);
            }
            catch (ConsumeException ex)
            {
               _logger.LogError(ex, "Error occured");
            }
            catch (OperationCanceledException)
            {
               break;
            }
         }

         c.Close();
      }

      private async Task ConsumeAsync(IConsumer<Ignore, string> consumer, CancellationToken stoppingToken)
      {
         var result = await Task.Run(() => consumer.Consume(stoppingToken));
         if(result != null)
         {
            using var scope = _serviceProvider.CreateScope();
            var messageConsumer = scope.ServiceProvider.GetService<IMessageConsumer<TMessage>>();
            var body = JsonConvert.DeserializeObject<TMessage>(result.Message.Value);
            await messageConsumer.OnMessageAsync(body);
         }
      }
   }
}
