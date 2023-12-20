using AElfIndexer.Client.Handlers;
using AutoMapper;
using Sample.Indexer.Entities;
using Sample.Indexer.GraphQL;

namespace Sample.Indexer;

public class TestGraphQLAutoMapperProfile : Profile
{
    public TestGraphQLAutoMapperProfile()
    {
        CreateMap<SampleIndexEntry, TransactionData>();
        CreateMap<LogEventContext, SampleIndexEntry>();
    }
}