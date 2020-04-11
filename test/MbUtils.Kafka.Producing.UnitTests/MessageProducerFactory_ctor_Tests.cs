using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MbUtils.Kafka.Producing.UnitTests
{
   public class MessageProducerFactory_ctor_Tests : MessageProducerFactoryTestBase
   {
      [Fact(DisplayName ="When input config is null, thne throw an exception")]
      public void When_input_config_is_null_Then_throw_exception()
      {
         // act & assert
         Assert.Throws<ArgumentNullException>("config", () => {
            new MessageProducerFactory<string>(
                  null,
                  _messageCreatorMock.Object,
                  _loggerFactoryMock.Object
               );
         });
      }

      [Fact(DisplayName = "When input messageCreator is null, then throw an exception")]
      public void When_input_messageCreator_is_null_Then_throw_exception()
      {
         // act & assert
         Assert.Throws<ArgumentNullException>("messageCreator", () => {
            new MessageProducerFactory<string>(
                  _optionsMock.Object,
                  null,
                  _loggerFactoryMock.Object
               );
         });
      }

      [Fact(DisplayName = "When input loggerFactory is null, then throw an exception")]
      public void When_input_loggerFactory_is_null_Then_throw_exception()
      {
         // act & assert
         Assert.Throws<ArgumentNullException>("loggerFactory", () => {
            new MessageProducerFactory<string>(
               _optionsMock.Object,
               _messageCreatorMock.Object,
               null
               );
         });
      }
   }
}
