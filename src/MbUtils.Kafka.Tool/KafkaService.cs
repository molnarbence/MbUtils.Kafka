using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace MbUtils.Kafka.Tool
{
   public class KafkaServiceConfig
   {
      public string BootstrapServers { get; set; }
   }

   public class KafkaService
   {
      private readonly ProducerBuilder<Null, string> _producerBuilder;

      public KafkaService(KafkaServiceConfig config)
      {
         var producerConfig = new ProducerConfig { BootstrapServers = config.BootstrapServers };
         _producerBuilder = new ProducerBuilder<Null, string>(producerConfig);
      }

      public Task ProduceContent(string topic, string path)
      {
         topic = string.IsNullOrEmpty(topic) ? "test" : topic;
         if(string.IsNullOrEmpty(path))
            return ProduceTestContent(topic);
         else
            return ProduceFromFileAsync(topic, path);
      }

      private Task ProduceFromFileAsync(string topic, string path)
        {
         var fileContent = File.ReadAllText(path);
         return ProduceContentInternal(topic, fileContent);
      }

      private Task ProduceTestContent(string topic)
      {
         var testContent = JsonSerializer.Serialize(new { Foo = new DateTime() });
         return ProduceContentInternal(topic, testContent);
      }

      private async Task ProduceContentInternal(string topic, string content)
      {
         var kafkaMessage = new Message<Null, string> { Value = content };

         using var producer = _producerBuilder.Build();
         var result = await producer.ProduceAsync(topic, kafkaMessage);
         Console.WriteLine($"Delivered content to topic: '{topic}', offset: '{result.TopicPartitionOffset}'");
      }
   }
}
