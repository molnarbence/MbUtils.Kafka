using System;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace MbUtils.Kafka.Tool.Commands
{
   [Command("produce")]
   [HelpOption]
   public class ProduceCommand
   {
      [Option(Description = "Path to file to be sent as message")]
      public string Path { get; set; }
      [Option(Description = "Topic name to be used to send message to")]
      public string Topic { get; set; }

      public async Task<int> OnExecuteAsync(KafkaService kafkaService, IReporter reporter, ILogger<ProduceCommand> logger)
      {
         try
         {
            await kafkaService.ProduceContent(Topic, Path);
            return 0;
         }
         catch (Exception ex)
         {
            reporter.Error(ex.Message);
            logger.LogError(ex, nameof(OnExecuteAsync));
            return 1;
         }
      }
   }
}
