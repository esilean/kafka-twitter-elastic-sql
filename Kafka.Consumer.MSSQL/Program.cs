using Confluent.Kafka;
using Kafka.Consumer.MSSQL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;

namespace Kafka.Consumer.MSSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            Migrate();
            Console.WriteLine("--------Iniciando MQSQL Consumer-----------------");
            Console.WriteLine(".................................................");
            Console.WriteLine("");

            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "kafka-twitter-mssql",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            var msSqlConsumer = new MSSQLConsumer(config);
            msSqlConsumer.Subscribe(new string[] { "twitter_tweets" });
            msSqlConsumer.Run(cts);

            Console.ReadKey();
        }

        private static void Migrate()
        {
            var context = new TwitterDbContext();
            context.Database.Migrate();
        }
    }
}
