using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MbUtils.Kafka.Producing.UnitTests
{
   public class MessageProducerFactory_Create_Tests : MessageProducerFactoryTestBase
   {
      [Fact(DisplayName = "Expect result to have correct type")]
      public void Expect_result_to_have_correct_type()
      {
         // act
         var result = _target.Create();

         // assert
         Assert.IsType<MessageProducer<string>>(result);
      }
   }
}
