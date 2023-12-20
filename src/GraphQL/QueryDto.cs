using GraphQL;
using Volo.Abp.Application.Dtos;

namespace Sample.Indexer.GraphQL;

public class QueryDto: PagedResultRequestDto
{
    
    [Name("playerAddress")]
    public string PlayerAddress { get; set; }
}