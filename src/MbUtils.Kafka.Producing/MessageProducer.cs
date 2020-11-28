using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MbUtils.Kafka.Producing
{
   public class MessageProducerConfig
   {
      public string BootstrapServers { get; set; }
   }

   public class MessageProducer<TValue> : IMessageProducer
   {
      private readonly IProducer<Null, TValue> _kafkaProducer;
      private readonly IMessageCreator<TValue> _messageCreator;
      protected readonly ILogger _logger;

      public MessageProducer(IProducerFactory<TValue> producerFactory, IMessageCreator<TValue> messageCreator, ILogger<MessageProducer<TValue>> logger)
      {
         if (producerFactory is null)
         {
            throw new ArgumentNullException(nameof(producerFactory));
         }

         _messageCreator = messageCreator ?? throw new ArgumentNullException(nameof(messageCreator));
         _logger = logger ?? throw new ArgumentNullException(nameof(logger));
         _kafkaProducer = producerFactory.Build();
      }

      public async Task ProduceAsync(string topic, object body)
      {
         try
         {
            var message = _messageCreator.CreateMessage(body);
            var result = await _kafkaProducer.ProduceAsync(topic, message);
            _logger.LogDebug("Delivered '{0}' to '{1}'", result.Value, result.TopicPartitionOffset);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex, nameof(ProduceAsync));
            throw ex;
         }
      }

      public void Produce(string topic, object body)
      {
         try
         {
            var message = _messageCreator.CreateMessage(body);
            _kafkaProducer.Produce(topic, message, DeliveryHandler);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex, nameof(Produce));
            throw ex;
         }
      }

      private void DeliveryHandler(DeliveryReport<Null, TValue> obj)
      {
         
      }

      public void Dispose()
      {
         _kafkaProducer.Dispose();
      }
   }
}
