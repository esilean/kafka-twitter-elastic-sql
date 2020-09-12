using Kafka.Producer.TwitterStream;
using Microsoft.Extensions.Configuration;
using System;

namespace Kafka.Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------Iniciando Stream do Twitter--------------");
            Console.WriteLine(".................................................");
            Console.WriteLine("");

            IConfiguration config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json", true, true)
                            .Build();

            var twitterConfig = new TwitterConfig
            {
                ConsumerKey = config["Twitter:ConsumerKey"],
                ConsumerSecret = config["Twitter:ConsumerSecret"],
                AccessToken = config["Twitter:AccessToken"],
                AccessTokenSecret = config["Twitter:AccessTokenSecret"]
            };

            var twitterStream = new TwitterStream.TwitterStream(twitterConfig);
            twitterStream.Execute(new string[] { "netflix" });

            Console.ReadKey();
        }
    }
}
