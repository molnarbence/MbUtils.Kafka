using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Moq;

namespace MbUtils.Kafka.Producing.UnitTests
{
   public abstract class MessageProducerMethodsTestBase : MessageProducerTestBase
   {
      protected readonly MessageProducer<string> _messageProducer;

      public MessageProducerMethodsTestBase()
      {
         // setup config
         _producerFactoryMock.Setup(x => x.Build()).Returns(_kafkaProducerMock.Object);

         // setup message creator
         
         _messageCreatorMock
            .Setup(x => x.CreateMessage(It.IsAny<object>()))
            .Returns<string>(x => new Message<Null, string> { Value = x });

         // new instance of message producer
         _messageProducer = new MessageProducer<string>(
             _producerFactoryMock.Object,
             _messageCreatorMock.Object,
             _loggerMock.Object);
      }
   }
}
