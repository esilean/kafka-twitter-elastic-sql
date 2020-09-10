using System;

namespace Kafka.Shared.Models
{
    public class Tweet
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public string FullText { get; set; }
        public string Language { get; set; }
        public string Source { get; set; }
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
