using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MbUtils.Kafka.Producing.UnitTests
{
   public class MessageProducer_ProduceAsync_Tests : MessageProducerMethodsTestBase
   {
      [Fact(DisplayName = "KafkaProducer is called with message from MessageCreator")]
      public async Task KafkaProducer_gets_MessageCreator_s_message()
      {
         // arrange
         var topic = "Test topic";
         var message = "test message";

         // act
         await _messageProducer.ProduceAsync(topic, message);

         // assert
         _kafkaProducerMock.Verify(x => x.ProduceAsync(
            "Test topic",
            It.Is<Message<Null, string>>(y => y.Value == message),
            default
            ));
      }

      [Fact(DisplayName = "When kafka producer throws an error, it logs an error")]
      public async Task When_producer_throws_Then_log_an_error()
      {
         // arrange
         var topic = "Test topic";
         var message = "test message";
         var expectedException = new Exception("Test exception");
         _kafkaProducerMock.Setup(x => x.ProduceAsync(
            It.IsAny<string>(),
            It.IsAny<Message<Null, string>>(),
            default
            )).Throws(expectedException);

         // act
         var thrownException = await Assert.ThrowsAsync<Exception>(async () =>
         {
            await _messageProducer.ProduceAsync(topic, message);
         });
         Assert.Equal(expectedException, thrownException);
         _loggerMock.Verify(x => x.Log(
             It.IsAny<LogLevel>(),
             It.IsAny<EventId>(),
             It.IsAny<It.IsAnyType>(),
             It.IsAny<Exception>(),
            (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()
            ), Times.Once());
      }
   }
}
