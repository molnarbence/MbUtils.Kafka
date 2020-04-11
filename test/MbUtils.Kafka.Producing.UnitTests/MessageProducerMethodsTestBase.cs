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
         // setup message creator
         
         _messageCreatorMock
            .Setup(x => x.CreateMessage(It.IsAny<object>()))
            .Returns<string>(x => new Message<Null, string> { Value = x });

         // setup kafka producer
         _kafkaProducerMock.Setup(x => x.ProduceAsync(
            It.IsAny<string>(),
            It.IsAny<Message<Null, string>>(),
            default
            )).Returns<string, Message<Null, string>, CancellationToken>((s, m, _) => 
            {
               var ret = new DeliveryResult<Null, string>
               {
                  Message = m,
                  Topic = "test topic",
                  Partition = Partition.Any,
                  Offset = Offset.Beginning
               };
               return Task.FromResult(ret);
            });

         // new instance of message producer
         _messageProducer = new MessageProducer<string>(
             _kafkaProducerMock.Object,
             _messageCreatorMock.Object,
             _loggerMock.Object);
      }
   }
}
