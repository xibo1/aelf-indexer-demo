using Xunit;

namespace Sample.Indexer.Orleans.TestBase;

[CollectionDefinition(Name)]
public class ClusterCollection:ICollectionFixture<ClusterFixture>
{
    private const string Name = "ClusterCollection";
}