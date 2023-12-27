using AElfIndexer.Client.Handlers;
using AutoMapper;
using Sample.Indexer.Entities;
using Sample.Indexer.GraphQL;

namespace Sample.Indexer;

// ReSharper disable once InconsistentNaming
// ReSharper disable once UnusedType.Global
public class TestGraphQLAutoMapperProfile : Profile
{
    public TestGraphQLAutoMapperProfile()
    {
        CreateMap<SampleIndexEntry, TransactionData>();
        CreateMap<LogEventContext, SampleIndexEntry>();
    }
}