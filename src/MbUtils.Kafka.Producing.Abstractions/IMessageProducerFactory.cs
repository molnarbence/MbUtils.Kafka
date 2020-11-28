using System;

namespace MbUtils.Kafka.Producing
{
   public interface IMessageProducerFactory : IDisposable
   {
      IMessageProducer Create();
   }
}
