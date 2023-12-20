using AElf.Contracts.HelloWorld;
using AElf.CSharp.Core.Extension;
using AElf.Types;
using AElfIndexer.Client;
using AElfIndexer.Client.Handlers;
using AElfIndexer.Grains.State.Client;
using Nethereum.Hex.HexConvertors.Extensions;
using Sample.Indexer.Entities;
using Sample.Indexer.Processors;
using Shouldly;
using Xunit;

namespace Sample.Indexer.Tests;

public class SampleProcessorTests: SampleIndexerTestBase
{
    private readonly IAElfIndexerClientEntityRepository<SampleIndexEntry, LogEventInfo> _sampleIndexRepository;

    public SampleProcessorTests()
    {
        _sampleIndexRepository = GetRequiredService<IAElfIndexerClientEntityRepository<SampleIndexEntry, LogEventInfo>>();
    }

    [Fact]
    public async Task HandleSampleEvent_Test()
    {
        const string chainId = "AELF";
        const string blockHash = "3c7c267341e9f097b0886c8a1661bef73d6bb4c30464ad73be714fdf22b09bdd";
        const string previousBlockHash = "9a6ef475e4c4b6f15c37559033bcfdbed34ca666c67b2ae6be22751a3ae171de";
        const string transactionId = "c09b8c142dd5e07acbc1028e5f59adca5b5be93a0680eb3609b773044a852c43";
        const long blockHeight = 200;
        var blockStateSetAdded = new BlockStateSet<LogEventInfo>
        {
            BlockHash = blockHash,
            BlockHeight = blockHeight,
            Confirmed = true,
            PreviousBlockHash = previousBlockHash
        };
        
        var blockStateSetTransaction = new BlockStateSet<TransactionInfo>
        {
            BlockHash = blockHash,
            BlockHeight = blockHeight,
            Confirmed = true,
            PreviousBlockHash = previousBlockHash
        };
        var blockStateSetKey = await InitializeBlockStateSetAsync(blockStateSetAdded, chainId);
        var blockStateSetKeyTransaction = await InitializeBlockStateSetAsync(blockStateSetTransaction, chainId);
        var sampleEvent = new SampleEvent
        {
            PlayerAddress = Address.FromPublicKey("AAA".HexToByteArray()).ToString()?.Trim('\"'),
            Timestamp = 1702968980,
            Amount = 100000000
        };
        var logEventInfo = new LogEventInfo
        {
            ExtraProperties = new Dictionary<string, string>
            {
                { "Indexed", sampleEvent.ToLogEvent().Indexed.ToString() ?? string.Empty },
                { "NonIndexed", sampleEvent.ToLogEvent().NonIndexed.ToBase64() }
            },
            BlockHeight = blockHeight,
            ChainId = chainId,
            BlockHash = blockHash,
            TransactionId = transactionId
        };
        var logEventContext = new LogEventContext
        {
            ChainId = chainId,
            BlockHeight = blockHeight,
            BlockHash = blockHash,
            PreviousBlockHash = previousBlockHash,
            TransactionId = transactionId,
            Params = "{ \"to\": \"ca\", \"symbol\": \"ELF\", \"amount\": \"100000000000\" }",
            To = "CAAddress",
            MethodName = "Played",
            ExtraProperties = new Dictionary<string, string>
            {
                { "TransactionFee", "{\"ELF\":\"30000000\"}" },
                { "ResourceFee", "{\"ELF\":\"30000000\"}" }
            },
            BlockTime = DateTime.UtcNow
        };
        var sampleProcessor = GetRequiredService<SampleTransactionProcessor>();
        await sampleProcessor.HandleEventAsync(logEventInfo, logEventContext);
        sampleProcessor.GetContractAddress(chainId);

        //step4: save blockStateSet into es
        await BlockStateSetSaveDataAsync<LogEventInfo>(blockStateSetKey);
        await BlockStateSetSaveDataAsync<TransactionInfo>(blockStateSetKeyTransaction);
        await Task.Delay(2000);
        
        var sampleIndexData = await _sampleIndexRepository.GetAsync(Address.FromPublicKey("AAA".HexToByteArray()).ToString()?.Trim('\"'));
        sampleIndexData.ShouldNotBeNull();
        sampleIndexData.Amount.ShouldBe(100000000);
    }
}