using MbUtils.Kafka.Consuming;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ConsoleAppBasic
{
   public class TestMessageConsumer : IMessageConsumer<TestMessage>
   {
      private readonly ILogger _logger;
      private readonly string _guid = Guid.NewGuid().ToString("N");

      public TestMessageConsumer(ILogger<TestMessageConsumer> logger)
      {
         _logger = logger;
      }
      public Task OnMessageAsync(TestMessage message)
      {
         _logger.LogInformation($"{_guid} {message.Foo}");
         return Task.CompletedTask;
      }
   }
}
