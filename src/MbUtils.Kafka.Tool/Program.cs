using System.Threading.Tasks;
using MbUtils.Extensions.CommandLineUtils;
using MbUtils.Kafka.Tool.Commands;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace MbUtils.Kafka.Tool
{
   [Command("kafka-tool")]
   [Subcommand(typeof(ProduceCommand))]
   class Program
   {
      static Task<int> Main(string[] args)
      {
         var wrapper = new CommandLineApplicationWrapper<Program>(args);

         wrapper.HostBuilder.ConfigureServices((_, services) => services
            .AddSingleton<KafkaService>()
            .AddSingleton<IReporter, ConsoleReporter>())
            .AddConfig<KafkaServiceConfig>(nameof(KafkaService));

         return wrapper.Execute();
      }

      public int OnExecute(CommandLineApplication app, IConsole console)
      {
         console.WriteLine("You must specify a subcommand");
         app.ShowHelp();
         return 1;
      }
   }
}
