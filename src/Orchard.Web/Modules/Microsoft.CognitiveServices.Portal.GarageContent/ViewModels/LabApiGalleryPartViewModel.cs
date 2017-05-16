using Orchard.ContentManagement;
using System.Collections.Generic;

namespace Microsoft.CognitiveServices.Portal.GarageContent.ViewModels
{
    public class LabApiGalleryPartViewModel
    {
        public string HeaderImageUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FAQName { get; set; }
        public Link LabTermsLink { get; set; }
        public IEnumerable<ContentItem> LabApis { get; set; }
        public string HtmlBody { get; set; }
    }

    public class Link
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
}