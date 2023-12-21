namespace Sample.Indexer;

public class ContractInfoOptions
{
    public List<ContractInfo> ContractInfos { get; set; }
}

public class ContractInfo
{
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public string ChainId { get; set; }
    // ReSharper disable once UnassignedGetOnlyAutoProperty
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string SampleContractAddress { get; set;}
}