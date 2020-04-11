using MbUtils.Kafka.Consuming;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace ConsoleAppBasic
{
   class Program
   {
      static async Task Main(string[] args)
      {
         var hostBuilder = new ConsumerHostBuilder(args)
            .UseConsumer<TestMessageConsumer, TestMessage>();

         await hostBuilder.RunConsoleAsync();
      }
   }
}
