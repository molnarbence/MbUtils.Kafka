using MbUtils.Kafka.Producing;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
   public static class MessageProducingServiceExtensions
   {
      public static IServiceCollection AddMessageProducing(this IServiceCollection services, IConfiguration configuration)
      {
         services.AddSingleton<IMessageProducerFactory, MessageProducerFactory<string>>();
         services.AddSingleton<IMessageCreator<string>, JsonMessageCreator>();

         services.Configure<MessageProducerConfig>(configuration.GetSection("MessageProducerConfig"));

         return services;
      }
   }
}
