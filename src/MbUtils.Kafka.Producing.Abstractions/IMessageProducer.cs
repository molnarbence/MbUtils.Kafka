using System;
using System.Threading.Tasks;

namespace MbUtils.Kafka.Producing
{
   public interface IMessageProducer : IDisposable
   {
      Task ProduceAsync(string topic, object body);
      void Produce(string topic, object body);
   }
}
