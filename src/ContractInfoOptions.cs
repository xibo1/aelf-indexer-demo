namespace Sample.Indexer;

public class ContractInfoOptions
{
    public List<ContractInfo> ContractInfos { get; set; }
}

public class ContractInfo
{
    public string ChainId { get; set; }
    // ReSharper disable once UnassignedGetOnlyAutoProperty
    public string SampleContractAddress { get; set;}
}