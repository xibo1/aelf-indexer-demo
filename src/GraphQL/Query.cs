using AElfIndexer.Client;
using AElfIndexer.Grains.State.Client;
using GraphQL;
using Nest;
using Sample.Indexer.Entities;
using Volo.Abp.ObjectMapping;

namespace Sample.Indexer.GraphQL;
public class Query
{
    public static async Task<SampleResultDto> SampleIndexerQuery(
    [FromServices] IAElfIndexerClientEntityRepository<SampleIndexEntry, LogEventInfo> repository,
    [FromServices] IObjectMapper objectMapper,  QueryDto dto)
    {
        var infoQuery = new List<Func<QueryContainerDescriptor<SampleIndexEntry>, QueryContainer>>();
        if (dto.PlayerAddress == null)
        {
            return new SampleResultDto();
        }
        infoQuery.Add(q => q.Terms(i => i.Field(f => f.FromAddress).Terms(dto.PlayerAddress)));
        var result = await repository.GetSortListAsync(
            f => f.Bool(b => b.Must(infoQuery)), 
            sortFunc: s => s.Descending(a => a.Timestamp));
        var dataList = objectMapper.Map<List<SampleIndexEntry>, List<TransactionData>>(result.Item2);
        var queryResult = new SampleResultDto
        {
            Data = dataList
        };
        return queryResult;
    }
}