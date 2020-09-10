using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace Kafka.Shared
{
    public abstract class KafkaConsumer<T> where T : class
    {

        public IConsumer<string, string> Consumer { get; set; }

        public KafkaConsumer(ConsumerConfig config)
        {
            Consumer = new ConsumerBuilder<string, string>(config).Build();
        }

        public abstract void Subscribe(string[] topics);

        public abstract void ConsumeResponse(T @object);

        public virtual void Run(CancellationTokenSource stoppingToken = default)
        {
            try
            {
                while (true)
                {
                    ConsumeResult<string, string> consumeResult = Consumer.Consume(stoppingToken.Token);
                    if (consumeResult.IsPartitionEOF)
                        continue;

                    var @object = JsonConvert.DeserializeObject<T>(consumeResult.Message.Value);
                    ConsumeResponse(@object);

                    Consumer.Commit();
                }
            }
            catch (OperationCanceledException)
            {
                Consumer.Close();
            }
        }
    }
}
