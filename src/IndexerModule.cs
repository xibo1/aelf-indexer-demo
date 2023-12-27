using AElfIndexer.Client;
using AElfIndexer.Client.Handlers;
using AElfIndexer.Grains.State.Client;
using Microsoft.Extensions.DependencyInjection;
using Sample.Indexer.GraphQL;
using Sample.Indexer.Handler;
using Sample.Indexer.Processors;
using Volo.Abp.Modularity;

namespace Sample.Indexer;

[DependsOn(typeof(AElfIndexerClientModule))]
public class SampleIndexerModule:AElfIndexerClientPluginBaseModule<SampleIndexerModule, IndexerSchema, Query>
{
    protected override void ConfigureServices(IServiceCollection serviceCollection)
    {
        var configuration = serviceCollection.GetConfiguration();
        serviceCollection.AddSingleton<IAElfLogEventProcessor<TransactionInfo>, SampleTransactionProcessor>();
        serviceCollection.AddTransient<IBlockChainDataHandler, SampleHandler>();
        Configure<ContractInfoOptions>(configuration.GetSection("ContractInfo"));
    }

    protected override string ClientId => "";
    protected override string Version => "";

}