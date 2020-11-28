using Confluent.Kafka;
using MbUtils.Kafka.Producing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
{
   public static class MessageProducingServiceExtensions
   {
      public static IServiceCollection AddMessageProducing(this IServiceCollection services, IConfiguration configuration)
      {
         services.AddSingleton<IMessageProducer, MessageProducer<string>>();
         services.AddSingleton<IMessageCreator<string>, JsonMessageCreator>();

         services.Configure<MessageProducerConfig>(configuration.GetSection("MessageProducerConfig"));

         services.AddSingleton<IProducerFactory<string>, ProducerFactory<string>>();

         return services;
      }
   }
}
