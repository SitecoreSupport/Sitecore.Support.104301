using Sitecore.ContentSearch;
using System.Linq;

namespace Sitecore.Support.ContentSearch
{
  public class SitecoreItemCrawler : Sitecore.ContentSearch.SitecoreItemCrawler
  {
    protected override void UpdateDependents(IProviderUpdateContext context, SitecoreIndexableItem indexable)
    {
      foreach (var uniqueId in GetIndexingDependencies(indexable).Where(i => !this.IsExcludedFromIndex(indexable, true)))
      {
        if (!this.CircularReferencesIndexingGuard.IsInProcessedList(uniqueId, this, context))
        {
          foreach (IProviderCrawler crawler in this.Index.Crawlers)
            crawler.Update(context, uniqueId, IndexingOptions.Default);
        }
      }
    }
  }
}