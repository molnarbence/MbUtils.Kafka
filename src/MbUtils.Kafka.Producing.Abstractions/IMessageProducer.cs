using System.Threading.Tasks;

namespace MbUtils.Kafka.Producing
{
   public interface IMessageProducer
   {
      Task ProduceAsync(string topic, object body);
      void Produce(string topic, object body);
   }
}
