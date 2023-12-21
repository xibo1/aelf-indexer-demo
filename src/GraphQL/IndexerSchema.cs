using AElfIndexer.Client.GraphQL;

namespace Sample.Indexer.GraphQL;

// ReSharper disable once ClassNeverInstantiated.Global
public class IndexerSchema : AElfIndexerClientSchema<Query>
{
    public IndexerSchema(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}