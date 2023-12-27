using GraphQL;
using Volo.Abp.Application.Dtos;

namespace Sample.Indexer.GraphQL;

public abstract class QueryDto: PagedResultRequestDto
{
    
    [Name("playerAddress")]
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string PlayerAddress { get; set; }
}

public class ResultDto
{
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public List<TransactionData> Data { get; set; }
}

public class TransactionData
{
    public string FromAddress { get; set; }
    
    public long Timestamp { get; set; }
    
    public long Amount { get; set; }
}