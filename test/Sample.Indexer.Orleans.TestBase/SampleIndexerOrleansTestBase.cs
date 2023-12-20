using Orleans.TestingHost;
using Sample.Indexer.TestBase;
using Volo.Abp.Modularity;

namespace Sample.Indexer.Orleans.TestBase;

public abstract class SampleIndexerOrleansTestBase<TStartupModule>:SampleIndexerTestBase<TStartupModule> 
    where TStartupModule : IAbpModule
{
    protected readonly TestCluster Cluster;

    protected SampleIndexerOrleansTestBase()
    {

        Cluster = GetRequiredService<ClusterFixture>().Cluster;
    }
}