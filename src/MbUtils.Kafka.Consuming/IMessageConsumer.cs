using System.Threading.Tasks;

namespace MbUtils.Kafka.Consuming
{
   public interface IMessageConsumer<TMessage>
   {
      Task OnMessageAsync(TMessage message);
   }
}