using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MbUtils.Kafka.Producing
{
   public class MessageProducerConfig
   {
      public string BootstrapServers { get; set; }
   }

   public class MessageProducerFactory<TValue> : IMessageProducerFactory
   {
      private readonly IOptions<MessageProducerConfig> _config;
      private readonly IMessageCreator<TValue> _messageCreator;
      private readonly ILoggerFactory _loggerFactory;

      public MessageProducerFactory(
         IOptions<MessageProducerConfig> config,
         IMessageCreator<TValue> messageCreator,
         ILoggerFactory loggerFactory)
      {
         _config = config ?? throw new System.ArgumentNullException(nameof(config));
         _messageCreator = messageCreator ?? throw new System.ArgumentNullException(nameof(messageCreator));
         _loggerFactory = loggerFactory ?? throw new System.ArgumentNullException(nameof(loggerFactory));
      }

      public IMessageProducer Create()
      {
         var producerConfig = new ProducerConfig { BootstrapServers = _config.Value.BootstrapServers };
         var kafkaProducer = new ProducerBuilder<Null, TValue>(producerConfig).Build();
         var ret = new MessageProducer<TValue>(
            kafkaProducer,
            _messageCreator,
            _loggerFactory.CreateLogger<MessageProducer<TValue>>());
         return ret;
      }
   }
}
