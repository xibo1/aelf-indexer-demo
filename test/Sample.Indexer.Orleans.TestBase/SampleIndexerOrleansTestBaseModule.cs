using Microsoft.Extensions.DependencyInjection;
using Orleans;
using Sample.Indexer.TestBase;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Sample.Indexer.Orleans.TestBase;

[DependsOn(typeof(AbpAutofacModule),
    typeof(AbpTestBaseModule),
    typeof(SampleIndexerTestBaseModule)
    )]
public class SampleIndexerOrleansTestBaseModule:AbpModule
{
    private readonly ClusterFixture _fixture = new();
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // var fixture = new ClusterFixture();
        context.Services.AddSingleton(_fixture);
        context.Services.AddSingleton<IClusterClient>(_ => _fixture.Cluster.Client);
    }
}