using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace MbUtils.Kafka.Producing
{
   public class MessageProducer<TValue> : IMessageProducer
   {
      private readonly IProducer<Null, TValue> _kafkaProducer;
      private readonly IMessageCreator<TValue> _messageCreator;
      protected readonly ILogger _logger;

      public MessageProducer(IProducer<Null, TValue> kafkaProducer, IMessageCreator<TValue> messageCreator, ILogger logger)
      {
         _kafkaProducer = kafkaProducer ?? throw new ArgumentNullException(nameof(kafkaProducer));
         _messageCreator = messageCreator ?? throw new ArgumentNullException(nameof(messageCreator));
         _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
         // DO NOTHING
      }
   }
}
