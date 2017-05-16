using Microsoft.CognitiveServices.Portal.GarageContent.Models;
using Microsoft.CognitiveServices.Portal.GarageContent.ViewModels;
using Orchard;

namespace Microsoft.CognitiveServices.Portal.GarageContent.Services
{
    public interface ILabApiDetailService : IDependency
    {
        LabApiDetailPartViewModel BuildEditorViewModel(LabApiDetailPart part);
        void UpdatePartsFromViewModel(LabApiDetailPart part, LabApiDetailPartViewModel viewModel);
    }
}