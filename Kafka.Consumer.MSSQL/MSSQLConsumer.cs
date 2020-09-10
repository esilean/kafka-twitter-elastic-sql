using Confluent.Kafka;
using Kafka.Consumer.MSSQL.Data;
using Kafka.Shared;
using Kafka.Shared.Models;
using System;

namespace Kafka.Consumer.MSSQL
{
    public class MSSQLConsumer : KafkaConsumer<Tweet>, IDisposable
    {
        private readonly TwitterDbContext _context;

        public MSSQLConsumer(ConsumerConfig config) : base(config)
        {
            _context = new TwitterDbContext();
        }

        public override void Subscribe(string[] topics)
        {
            Consumer.Subscribe(topics);
        }

        public override void ConsumeResponse(Tweet tweet)
        {
            try
            {
                _context.Tweets.AddAsync(tweet);
                _context.SaveChangesAsync();

                Console.WriteLine("");
                Console.WriteLine("----MSSQL Tweet----");
                Console.WriteLine("");
                Console.WriteLine($"Id: {tweet.Id}");
                Console.WriteLine($"Tweet: {tweet.Text}");
                Console.WriteLine("");
                Console.WriteLine("----MSSQL Fim------");
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
