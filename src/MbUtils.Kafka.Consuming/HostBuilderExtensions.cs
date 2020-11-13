using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using MbUtils.Kafka.Consuming;
using MbUtils.Kafka.Consuming.Configs;

namespace Microsoft.Extensions.Hosting
{
   public static class HostBuilderExtensions
   {
      public static IHostBuilder UseConsumer<TConsumer, TMessage>(this IHostBuilder hostBuilder)
      {
         return hostBuilder.ConfigureServices((context, services) =>
         {
            services.Configure<KafkaConsumerConfig>(context.Configuration.GetSection("KafkaConsumer"));
            services.AddScoped(typeof(IMessageConsumer<TMessage>), typeof(TConsumer));
            services.AddHostedService<ConsumerHostedService<TMessage>>();
         });
      }
   }
}
