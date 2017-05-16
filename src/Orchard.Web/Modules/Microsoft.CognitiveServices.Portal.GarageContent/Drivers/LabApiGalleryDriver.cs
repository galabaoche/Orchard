using Microsoft.CognitiveServices.Portal.GarageContent.Models;
using Microsoft.CognitiveServices.Portal.GarageContent.Services;
using Orchard.ContentManagement.Drivers;

namespace Microsoft.CognitiveServices.Portal.GarageContent.Drivers
{

    public class ApiGalleryDriver : ContentPartDriver<LabApiGalleryPart>
    {
        private readonly ILabApiGalleryService labApiGalleryService;
        public ApiGalleryDriver(ILabApiGalleryService labApiGalleryService)
        {
            this.labApiGalleryService = labApiGalleryService;
        }

        protected override DriverResult Display(LabApiGalleryPart part, string displayType, dynamic shapeHelper)
        {
            dynamic contentItem = (dynamic)part.ContentItem;
            var viewModel = labApiGalleryService.BuildLabApiGalleryDisplayViewModel(contentItem);
            return ContentShape("Parts_LabApiGallery",
                () =>
                {
                    var shape = shapeHelper.Parts_LabApiGalleryPart();
                    shape.ContentPart = part;
                    shape.ViewModel = viewModel;

                    return shape;
                });
        }
    }
}