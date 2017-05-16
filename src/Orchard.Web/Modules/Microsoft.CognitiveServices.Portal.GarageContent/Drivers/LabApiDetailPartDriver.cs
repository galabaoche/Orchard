using Microsoft.CognitiveServices.Portal.GarageContent.Models;
using Microsoft.CognitiveServices.Portal.GarageContent.Services;
using Microsoft.CognitiveServices.Portal.GarageContent.ViewModels;
using Orchard;
using Orchard.Autoroute.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using Orchard.MediaLibrary.Services;
using Orchard.UI.Notify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Microsoft.CognitiveServices.Portal.GarageContent.Drivers
{
    public class LabApiDetailPartDriver : ContentPartDriver<LabApiDetailPart>
    {
        private IOrchardServices orchardServices;
        private ILabApiDetailService labDetailService;
        private IMediaLibraryService mediaLibraryService;
        public LabApiDetailPartDriver(IOrchardServices orchardServices, ILabApiDetailService labDetailService, IMediaLibraryService mediaLibraryService)
        {
            this.orchardServices = orchardServices;
            this.labDetailService = labDetailService;
            this.mediaLibraryService = mediaLibraryService;
        }
        public Localizer T { get; set; }
        protected override DriverResult Editor(LabApiDetailPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_LabApiDetail_Edit", () => shapeHelper.EditorTemplate(
                 TemplateName: "Parts/LabApiDetail",
                 Model: this.labDetailService.BuildEditorViewModel(part),
                 Prefix: Prefix));
        }

        protected override DriverResult Editor(LabApiDetailPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            LabApiDetailPartViewModel viewModel = this.labDetailService.BuildEditorViewModel(part);
            if (!updater.TryUpdateModel(viewModel, Prefix, null, null))
            {
                this.orchardServices.Notifier.Error(T("Please enter all the required fields and submit again"));
            }

            if (part.ContentItem != null)
            {
                this.labDetailService.UpdatePartsFromViewModel(part, viewModel);
            }

            return Editor(part, shapeHelper);
        }

        protected override DriverResult Display(LabApiDetailPart part, string displayType, dynamic shapeHelper)
        {
            if (string.Equals(displayType, "Detail", StringComparison.OrdinalIgnoreCase))
            {
                return ContentShape("Parts_LabApiDetail",
                () => shapeHelper.Parts_LabApiDetailPart(
                    Title: part.Title,
                    Description: part.Description,
                    DocumentationLink: part.DocumentationLink,
                    SubscribeLink: part.SubscribeLink,
                    SDKLink: part.SDKLink,
                    Weight: part.Weight,
                    LabApiStatus: part.LabApiStatus,
                    Features: GetFeatureViewModels(part.Features),
                    HtmlBody: (part.ContentItem as dynamic).BodyPart.Text,
                    HeaderImageUrl: string.IsNullOrEmpty(part.HeaderImageUrl) ? string.Empty : mediaLibraryService.GetMediaPublicUrl("Media", "") + part.HeaderImageUrl.Replace(mediaLibraryService.GetMediaPublicUrl("Media", ""), ""),
                    ApiReferenceUrl: part.ApiReferenceUrl));
            }
            else if (string.Equals(displayType, "Gallery", StringComparison.OrdinalIgnoreCase))
            {
                return ContentShape("Parts_LabApiDetail_Gallery",
                    () => shapeHelper.Parts_LabApiDetailPart_Gallery(
                        ProductUrl: part.ContentItem.As<AutoroutePart>().DisplayAlias,
                        Title: part.Title,
                        LabApiStatus: part.LabApiStatus,
                        GalleryImageUrl: string.IsNullOrEmpty(part.GalleryImageUrl) ? GetLabApiDefaultIconUrl() : mediaLibraryService.GetMediaPublicUrl("Media", "") + part.GalleryImageUrl.Replace(mediaLibraryService.GetMediaPublicUrl("Media", ""), ""), 
                        Caption: part.Caption,
                        SubscribeLink: part.SubscribeLink,
                        SdkLink: part.SDKLink
                        ));
            }
            else
            {
                return null;
            }
        }
        private string GetLabApiDefaultIconUrl()
        {
            HttpRequestBase request = orchardServices.WorkContext.HttpContext.Request;
            return request.Url.Scheme + "://" + request.Url.Authority + request.ApplicationPath + "/Modules/Microsoft.CognitiveServices.Portal.GarageContent/Images/labapi_icon.svg";
        }

        private IEnumerable<LabFeatureDisplayViewModel> GetFeatureViewModels(IEnumerable<LabFeatureRecord> featureRecords)
        {
            foreach (LabFeatureRecord record in featureRecords.OrderBy(f => f.SortOrder))
            {
                yield return new LabFeatureDisplayViewModel(record);
            }
        }
    }
}