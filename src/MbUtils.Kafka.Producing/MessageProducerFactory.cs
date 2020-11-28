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
      private readonly IProducer<Null, TValue> _kafkaProducer;

      public MessageProducerFactory(
         IOptions<MessageProducerConfig> config,
         IMessageCreator<TValue> messageCreator,
         ILoggerFactory loggerFactory)
      {
         _config = config ?? throw new System.ArgumentNullException(nameof(config));
         _messageCreator = messageCreator ?? throw new System.ArgumentNullException(nameof(messageCreator));
         _loggerFactory = loggerFactory ?? throw new System.ArgumentNullException(nameof(loggerFactory));

         var producerConfig = new ProducerConfig { BootstrapServers = _config.Value.BootstrapServers };
         _kafkaProducer = new ProducerBuilder<Null, TValue>(producerConfig).Build();
      }

      public IMessageProducer Create()
      {
         var ret = new MessageProducer<TValue>(
            _kafkaProducer,
            _messageCreator,
            _loggerFactory.CreateLogger<MessageProducer<TValue>>());
         return ret;
      }
   }
}
