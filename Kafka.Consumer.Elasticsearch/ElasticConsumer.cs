using Confluent.Kafka;
using Elasticsearch.Net;
using Kafka.Consumer.Elasticsearch.ElasticSearch;
using Kafka.Shared;
using Kafka.Shared.Models;
using System;

namespace Kafka.Consumer.Elasticsearch
{
    public class ElasticConsumer : KafkaConsumer<Tweet>, IDisposable
    {
        private readonly ElClient _elClient;
        public ElasticConsumer(string elasticURL, ConsumerConfig config) : base(config)
        {
            _elClient = new ElClient(elasticURL);
        }

        public override void Subscribe(string[] topics)
        {
            Consumer.Subscribe(topics);
        }

        public override void ConsumeResponse(Tweet tweet)
        {
            try
            {
                _elClient.Client.Index<StringResponse>("twitter", tweet.Id.ToString(), PostData.Serializable(tweet));

                Console.WriteLine("");
                Console.WriteLine("----Elastic Tweet----");
                Console.WriteLine("");
                Console.WriteLine($"Id: {tweet.Id}");
                Console.WriteLine($"Tweet: {tweet.Text}");
                Console.WriteLine("");
                Console.WriteLine("----Elastic Fim----");
                Console.WriteLine("");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Dispose()
        {
            Consumer.Dispose();
        }

    }
}
