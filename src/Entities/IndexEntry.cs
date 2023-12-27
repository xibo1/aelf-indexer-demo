using AElf.Indexing.Elasticsearch;
using AElfIndexer.Client;
using Nest;

namespace Sample.Indexer.Entities;
public class SampleIndexEntry : AElfIndexerClientEntity<string>, IIndexBuild
{
    [Keyword]
    public string FromAddress { get; set; }
    
    public long Timestamp { get; set; }
    
    public long Amount { get; set; }
   
    //Define it according to your own usage requirements.
}