using AElfIndexer.Client;
using AElfIndexer.Client.Handlers;
using AElfIndexer.Grains.State.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sample.Indexer.Entities;
using AElf.Contracts.HelloWorld;
using IObjectMapper = Volo.Abp.ObjectMapping.IObjectMapper;

namespace Sample.Indexer.Processors;
public class SampleTransactionProcessor : AElfLogEventProcessorBase<SampleEvent,TransactionInfo>
{
    private readonly IAElfIndexerClientEntityRepository<SampleIndexEntry, TransactionInfo> _sampleIndexRepository;
    private readonly ContractInfoOptions _contractInfoOptions;
    private readonly IObjectMapper _objectMapper;

    public SampleTransactionProcessor(ILogger<SampleTransactionProcessor> logger,
        IAElfIndexerClientEntityRepository<SampleIndexEntry, TransactionInfo> sampleIndexRepository,
        IOptionsSnapshot<ContractInfoOptions> contractInfoOptions,
        IObjectMapper objectMapper) : base(logger)
    {
        _sampleIndexRepository = sampleIndexRepository;
        _objectMapper = objectMapper;
        _contractInfoOptions = contractInfoOptions.Value;
    }

    public override string GetContractAddress(string chainId)
    {
        return _contractInfoOptions.ContractInfos.First(c=>c.ChainId == chainId).SampleContractAddress;
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
        _objectMapper.Map(context, indexEntry);
        await _sampleIndexRepository.AddOrUpdateAsync(indexEntry);
    }
}