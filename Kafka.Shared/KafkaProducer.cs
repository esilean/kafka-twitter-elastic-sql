using Confluent.Kafka;
using System.Threading.Tasks;

namespace Kafka.Shared
{
    public abstract class KafkaProducer<T> where T : class
    {
        protected readonly IProducer<int, string> Producer;

        public KafkaProducer(ProducerConfig config)
        {
            Producer = new ProducerBuilder<int, string>(config).Build();
        }

        public abstract Task Send(T @object);

    }
}
