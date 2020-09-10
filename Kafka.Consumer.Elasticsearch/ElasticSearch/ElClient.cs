using Elasticsearch.Net;

using System;

namespace Kafka.Consumer.Elasticsearch.ElasticSearch
{
    public class ElClient
    {

        public readonly IElasticLowLevelClient Client;

        public ElClient(string elasticURL)
        {
            var uri = new Uri(elasticURL);
            var settings = new ConnectionConfiguration(uri);
            Client = new ElasticLowLevelClient(settings);
        }
    }
}
