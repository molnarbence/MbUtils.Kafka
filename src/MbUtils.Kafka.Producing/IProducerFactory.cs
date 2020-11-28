using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;

namespace MbUtils.Kafka.Producing
{
   public interface IProducerFactory<TValue> : IDisposable
   {
      IProducer<Null, TValue> Build();
   }
}
