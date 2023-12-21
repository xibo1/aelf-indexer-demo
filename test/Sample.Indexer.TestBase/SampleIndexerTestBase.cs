using Volo.Abp;
using Volo.Abp.Modularity;
using Volo.Abp.Testing;

namespace Sample.Indexer.TestBase;

/* All test classes are derived from this class, directly or indirectly.
     */
public class SampleIndexerTestBase<TStartupModule> : AbpIntegratedTest<TStartupModule> 
    where TStartupModule : IAbpModule
{
    protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
    {
        options.UseAutofac();
    }
}