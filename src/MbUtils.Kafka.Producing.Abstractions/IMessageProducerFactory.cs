namespace MbUtils.Kafka.Producing
{
   public interface IMessageProducerFactory
   {
      IMessageProducer Create();
   }
}
