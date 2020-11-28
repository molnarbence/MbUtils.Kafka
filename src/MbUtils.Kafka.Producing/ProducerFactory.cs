using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace MbUtils.Kafka.Producing
{
   public class ProducerFactory<TValue> : IProducerFactory<TValue>
   {
      private readonly IProducer<Null, TValue> _producer;
      public ProducerFactory(IOptions<MessageProducerConfig> config)
      {
         var producerConfig = new ProducerConfig { BootstrapServers = config.Value.BootstrapServers };
         _producer = new ProducerBuilder<Null, TValue>(producerConfig).Build();
      }
      public IProducer<Null, TValue> Build() => _producer;

      public void Dispose()
      {
         _producer.Flush();
         _producer.Dispose();
      }
   }
}
