using AElfIndexer.Client.GraphQL;

namespace Sample.Indexer.GraphQL;

public class IndexerSchema : AElfIndexerClientSchema<Query>
{
    public IndexerSchema(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}