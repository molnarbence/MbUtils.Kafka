using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using MbUtils.Kafka.Consuming;

namespace Microsoft.Extensions.Hosting
{
   public static class HostBuilderExtensions
   {
      public static IHostBuilder UseConsumer<TConsumer, TMessage>(this IHostBuilder hostBuilder)
      {
         return hostBuilder.ConfigureServices(collection =>
         {
            collection.AddScoped(typeof(IMessageConsumer<TMessage>), typeof(TConsumer));
            collection.AddHostedService<ConsumerHostedService<TMessage>>();
         });
      }
   }
}