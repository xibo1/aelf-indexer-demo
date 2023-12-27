using Orleans.TestingHost;
using Sample.Indexer.TestBase;
using Volo.Abp.Modularity;
// ReSharper disable VirtualMemberCallInConstructor

namespace Sample.Indexer.Orleans.TestBase;

public abstract class SampleIndexerOrleansTestBase<TStartupModule>:SampleIndexerTestBase<TStartupModule> 
    where TStartupModule : IAbpModule
{
    // ReSharper disable once NotAccessedField.Global
    protected readonly TestCluster Cluster;

    protected SampleIndexerOrleansTestBase()
    {

        Cluster = GetRequiredService<ClusterFixture>().Cluster;
    }
}