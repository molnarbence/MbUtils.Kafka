using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MbUtils.Kafka.Consuming.Tests.ConsumerHostBuilder_Tests
{
   public class Ctor_Tests
   {
      [Fact]
      public void When_args_null_Then_throw_exception()
      {
         // arrange
         string[] args = null;

         // act & assert
         Assert.Throws<ArgumentNullException>(() => new ConsumerHostBuilder(args));
      }

      [Fact]
      public void When_args_has_value_Then_Ok()
      {
         // arrange
         string[] args = new[] { "foo", "bar" };

         // act
         new ConsumerHostBuilder(args);
      }
   }
}
