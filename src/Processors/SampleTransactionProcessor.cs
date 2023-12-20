using AElfIndexer.Client;
using AElfIndexer.Client.Handlers;
using AElfIndexer.Grains.State.Client;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sample.Indexer.Entities;
using Volo.Abp.ObjectMapping;
using AElf.Contracts.HelloWorld;

namespace Sample.Indexer.Processors;
public class SampleTransactionProcessor : SampleProcessorBase<SampleEvent>
{
    private readonly IAElfIndexerClientEntityRepository<SampleIndexEntry, TransactionInfo> _sampleIndexRepository;

    public SampleTransactionProcessor(ILogger<SampleTransactionProcessor> logger,
        IAElfIndexerClientEntityRepository<SampleIndexEntry, TransactionInfo> sampleIndexRepository,
        IOptionsSnapshot<ContractInfoOptions> contractInfoOptions,
        IObjectMapper objectMapper) : base(logger, objectMapper, contractInfoOptions)
    {
        _sampleIndexRepository = sampleIndexRepository;
    }

    public override string GetContractAddress(string chainId)
    {
        return ContractInfoOptions.ContractInfos.First(c=>c.ChainId == chainId).SampleContractAddress;
    }

    protected override async Task HandleEventAsync(SampleEvent eventValue, LogEventContext context)
    {
        if (eventValue.PlayerAddress == null)
        {
            return;
        }
        
        var indexEntry = new SampleIndexEntry
        {
            Id = eventValue.PlayerAddress,
            FromAddress = eventValue.PlayerAddress,
            Timestamp = eventValue.Timestamp,
            Amount = eventValue.Amount
        };
        ObjectMapper.Map(context, indexEntry);
        await _sampleIndexRepository.AddOrUpdateAsync(indexEntry);
    }
}