namespace MbUtils.Kafka.Consuming.Configs
{
   internal class KafkaConsumerConfig
   {
      public string GroupId { get; set; }
      public string BootstrapServers { get; set; }
      public string AutoOffsetReset { get; set; }
      public string Topic { get; set; }
      public int DelayMs { get; set; }
   }
}
