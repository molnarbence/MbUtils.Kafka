using System;
using Xunit;

namespace MbUtils.Kafka.Producing.UnitTests
{
   public class MessageProducer_ctor_Tests : MessageProducerTestBase
   {
      [Fact(DisplayName = "When input producerFactory is null, then throw an exception")]
      public void When_producer_is_null_Throw_exception()
      {
         // act
         Assert.Throws<ArgumentNullException>("producerFactory", () =>
         {
            new MessageProducer<string>(
                   null,
                   _messageCreatorMock.Object,
                   _loggerMock.Object);
         });
      }

      [Fact(DisplayName = "When input message creator is null, then throw an exception")]
      public void When_messageCreator_is_null_Throw_exception()
      {
         // act
         Assert.Throws<ArgumentNullException>("messageCreator", () =>
         {
            new MessageProducer<string>(
                   _producerFactoryMock.Object,
                   null,
                   _loggerMock.Object);
         });
      }

      [Fact(DisplayName = "When input logger is null, then throw an exception")]
      public void When_logger_is_null_Throw_exception()
      {
         // act
         Assert.Throws<ArgumentNullException>("logger", () =>
         {
            new MessageProducer<string>(
                   _producerFactoryMock.Object,
                   _messageCreatorMock.Object,
                   null);
         });
      }
   }
}
