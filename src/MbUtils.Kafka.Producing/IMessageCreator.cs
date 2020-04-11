using Confluent.Kafka;

namespace MbUtils.Kafka.Producing
{
   public interface IMessageCreator<TValue>
   {
      Message<Null, TValue> CreateMessage(object body);
   }
}
