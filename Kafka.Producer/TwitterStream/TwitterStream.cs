using Confluent.Kafka;
using System;
using Tweetinvi;
using Tweetinvi.Models;

namespace Kafka.Producer.TwitterStream
{
    public class TwitterStream
    {
        private readonly TwitterProducer _twitterProducer;
        private readonly TwitterConfig _twitterConfig;

        public TwitterStream(TwitterConfig twitterConfig)
        {
            _twitterConfig = twitterConfig;

            var config = new ProducerConfig { 
                BootstrapServers = "localhost:9092"
            };


            //safer producer
            config.Acks = Acks.All;
            config.EnableIdempotence = true;
            config.MessageSendMaxRetries = 10000;
            config.MaxInFlight = 5;

            //melhorar taxa de transferencia
            config.CompressionType = CompressionType.Snappy;
            config.LingerMs = 20;
            config.BatchSize = 32 * 1024; // 32 KB

            _twitterProducer = new TwitterProducer("twitter_tweets", config);
        }


        public void Execute(string[] msgs)
        {
            try
            {

                var creds = new TwitterCredentials(_twitterConfig.ConsumerKey, _twitterConfig.ConsumerSecret,
                    _twitterConfig.AccessToken,
                    _twitterConfig.AccessTokenSecret);

                var stream = Stream.CreateFilteredStream(creds);
                foreach (var msg in msgs)
                    stream.AddTrack(msg);

                stream.MatchingTweetReceived += async (sender, args) =>
                {
                    var tweet = new Shared.Models.Tweet
                    {
                        Id = args.Tweet.Id,
                        Text = args.Tweet.Text,
                        FullText = args.Tweet.FullText,
                        Language = args.Tweet.Language.GetValueOrDefault().ToString(),
                        Source = args.Tweet.Source,
                        Url = args.Tweet.Url,
                        CreatedAt = args.Tweet.CreatedAt,
                    };

                    Console.WriteLine("");
                    Console.WriteLine("----Twitter Tweet----");
                    Console.WriteLine("");
                    Console.WriteLine($"Id: {tweet.Id}");
                    Console.WriteLine($"Tweet: {tweet.Text}");
                    Console.WriteLine("");
                    Console.WriteLine("----Twitter Fim------");
                    Console.WriteLine("");

                    await _twitterProducer.Send(tweet);
                };

                stream.StartStreamMatchingAllConditions();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }


}
