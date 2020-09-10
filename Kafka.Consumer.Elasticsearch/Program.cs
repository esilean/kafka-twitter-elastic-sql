using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;

namespace Kafka.Consumer.Elasticsearch
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("--------Iniciando Elastic Search Consumer--------");
            Console.WriteLine(".................................................");
            Console.WriteLine("");

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "kafka-twitter-elasticsearch",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            IConfiguration config = new ConfigurationBuilder()
                                        .AddJsonFile("appsettings.json", true, true)
                                        .Build();

            var elasticConsumer = new ElasticConsumer(config["ElasticURL"], consumerConfig);
            elasticConsumer.Subscribe(new string[] { "twitter_tweets" });
            elasticConsumer.Run(cts);

            Console.ReadKey();
        }


    }
}
