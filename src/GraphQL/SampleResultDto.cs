namespace Sample.Indexer.GraphQL;

public class SampleResultDto
{
    public List<TransactionData> Data { get; set; }
}

public class TransactionData
{
    public string FromAddress { get; set; }
    
    public long Timestamp { get; set; }
    
    public long Amount { get; set; }
}

