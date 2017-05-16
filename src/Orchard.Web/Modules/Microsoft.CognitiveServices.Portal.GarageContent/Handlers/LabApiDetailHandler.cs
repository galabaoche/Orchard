using Microsoft.CognitiveServices.Portal.GarageContent.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace Microsoft.CognitiveServices.Portal.Content.Handlers
{
    public class LabApiDetailHandler : ContentHandler
    {
        public LabApiDetailHandler(IRepository<LabApiDetailPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }

        protected override void GetItemMetadata(GetContentItemMetadataContext context)
        {
            LabApiDetailPart LabApiDetailPart = context.ContentItem.As<LabApiDetailPart>();
            if (LabApiDetailPart != null)
            {
                context.Metadata.DisplayText = LabApiDetailPart.Title;
            }
            base.GetItemMetadata(context);
        }
    }
}