using Microsoft.CognitiveServices.Portal.GarageContent.ViewModels;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Localization.Services;

namespace Microsoft.CognitiveServices.Portal.GarageContent.Services
{
    public class LabApiGalleryService : ILabApiGalleryService
    {
        private readonly IOrchardServices orchardServices;
        private readonly ICultureFilter cultureFilter;
        public LabApiGalleryService(IOrchardServices orchardServices, ICultureFilter cultureFilter)
        {
            this.orchardServices = orchardServices;
            this.cultureFilter = cultureFilter;
        }
        public LabApiGalleryPartViewModel BuildLabApiGalleryDisplayViewModel(dynamic contentItem)
        {
            Link labTermsLink = new Link()
            {
                Text = contentItem.LabApiGalleryPart.LabTermsLink.Text,
                Value = contentItem.LabApiGalleryPart.LabTermsLink.Value,
            };
            LabApiGalleryPartViewModel viewModel = new LabApiGalleryPartViewModel()
            {
                HeaderImageUrl = contentItem.LabApiGalleryPart.HeaderImageUrl.FirstMediaUrl,
                Title = contentItem.LabApiGalleryPart.Title.Value,
                Description = contentItem.LabApiGalleryPart.Description.Value,
                FAQName = contentItem.LabApiGalleryPart.FAQName.Value,
                LabTermsLink = labTermsLink,
                LabApis = cultureFilter.FilterCulture(orchardServices.ContentManager.Query("LabApiDetailPage"), orchardServices.WorkContext.CurrentCulture).List(),
                HtmlBody = contentItem.BodyPart.Text,
            };
            return viewModel;
        }
    }
}