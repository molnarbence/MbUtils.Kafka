using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Moq;

namespace MbUtils.Kafka.Producing.UnitTests
{
   public abstract class MessageProducerTestBase
   {
      protected readonly Mock<IProducer<Null, string>> _kafkaProducerMock = new Mock<IProducer<Null, string>>();
      protected readonly Mock<IMessageCreator<string>> _messageCreatorMock = new Mock<IMessageCreator<string>>();
      protected readonly Mock<ILogger> _loggerMock = new Mock<ILogger>();
   }
}
