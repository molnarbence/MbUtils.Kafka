using Confluent.Kafka;
using System.Text.Json;

namespace MbUtils.Kafka.Producing
{
   public class JsonMessageCreator : IMessageCreator<string>
   {
      public Message<Null, string> CreateMessage(object body)
      {
         return new Message<Null, string> { Value = JsonSerializer.Serialize(body) };
      }
   }
}
