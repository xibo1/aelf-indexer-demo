using AElf.CSharp.Core;
using AElfIndexer.Client.Handlers;
using AElfIndexer.Grains.State.Client;
using Sample.Indexer.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Volo.Abp.ObjectMapping;

namespace Sample.Indexer.Processors;

public abstract class SampleProcessorBase<TEvent> : AElfLogEventProcessorBase<TEvent,TransactionInfo> where TEvent : IEvent<TEvent>, new()
{
    protected readonly ContractInfoOptions ContractInfoOptions;
    protected readonly IObjectMapper ObjectMapper;

    protected SampleProcessorBase(ILogger<SampleProcessorBase<TEvent>> logger,
        IObjectMapper objectMapper, IOptionsSnapshot<ContractInfoOptions> contractInfoOptions) : base(logger)
    {
        ObjectMapper = objectMapper;
        ContractInfoOptions = contractInfoOptions.Value;
    }
}