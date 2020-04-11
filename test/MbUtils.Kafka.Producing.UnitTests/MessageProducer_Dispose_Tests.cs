using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Xunit;

namespace MbUtils.Kafka.Producing.UnitTests
{
   public class MessageProducer_Dispose_Tests : MessageProducerMethodsTestBase
   {
      [Fact(DisplayName ="Call kafkaProducer's Dispose")]
      public void Call_kafkaProducer_dispose()
      {
         // act
         _messageProducer.Dispose();

         // assert
         _kafkaProducerMock.Verify(x => x.Dispose(), Times.Once());
      }
   }
}
