using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace MbUtils.Kafka.Producing.UnitTests
{
   public abstract class MessageProducerFactoryTestBase
   {
      protected readonly MessageProducerFactory<string> _target;

      protected readonly Mock<IOptions<MessageProducerConfig>> _optionsMock;
      protected readonly Mock<IMessageCreator<string>> _messageCreatorMock;
      protected readonly Mock<ILoggerFactory> _loggerFactoryMock;

      protected MessageProducerFactoryTestBase()
      {
         _optionsMock = new Mock<IOptions<MessageProducerConfig>>();
         var opts = new MessageProducerConfig { BootstrapServers = "bootstrapservers" };
         _optionsMock.SetupGet(x => x.Value).Returns(opts);

         _messageCreatorMock = new Mock<IMessageCreator<string>>();
         var kafkaMessage = new Message<Null, string> { Value = "test message" };
         _messageCreatorMock
            .Setup(x => x.CreateMessage(It.IsAny<object>()))
            .Returns(kafkaMessage);

         _loggerFactoryMock = new Mock<ILoggerFactory>();

         _target = new MessageProducerFactory<string>(
            _optionsMock.Object,
            _messageCreatorMock.Object,
            _loggerFactoryMock.Object
            );
      }
   }
}
