using Microsoft.CognitiveServices.Portal.GarageContent.ViewModels;
using Orchard;

namespace Microsoft.CognitiveServices.Portal.GarageContent.Services
{
    public interface ILabApiGalleryService : IDependency
    {
        LabApiGalleryPartViewModel BuildLabApiGalleryDisplayViewModel(dynamic contentItem);
    }
}