using Confluent.Kafka;
using Kafka.Shared;
using Kafka.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Kafka.Producer
{
    public class TwitterProducer : KafkaProducer<Tweet>
    {
        private readonly string _topic;

        public TwitterProducer(string topic, ProducerConfig config) : base(config)
        {
            _topic = topic;
        }

        public override async Task Send(Tweet @object)
        {
            try
            {
                var value = JsonConvert.SerializeObject(@object);

                var message = new Message<int, string>
                {
                    Key = new Random().Next(0, 5),
                    Value = value
                };

                await Producer.ProduceAsync(_topic, message);
            }
            catch (ProduceException<int, string> e)
            {
                Console.WriteLine($"Delivery failed: {e.Error.Reason}");
            }
        }
    }
}
