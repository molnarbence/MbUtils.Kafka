using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace MbUtils.Kafka.Producing.UnitTests
{
   public abstract class MessageProducerTestBase
   {
      protected readonly Mock<IProducer<Null, string>> _kafkaProducerMock = new Mock<IProducer<Null, string>>();
      protected readonly Mock<IProducerFactory<string>> _producerFactoryMock = new Mock<IProducerFactory<string>>();
      protected readonly Mock<IMessageCreator<string>> _messageCreatorMock = new Mock<IMessageCreator<string>>();
      protected readonly Mock<ILogger<MessageProducer<string>>> _loggerMock = new Mock<ILogger<MessageProducer<string>>>();
   }
}
