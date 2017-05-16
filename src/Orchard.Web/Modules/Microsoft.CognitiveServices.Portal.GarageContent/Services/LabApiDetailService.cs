using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CognitiveServices.Portal.GarageContent.Models;
using Microsoft.CognitiveServices.Portal.GarageContent.ViewModels;
using Orchard.Data;
using Newtonsoft.Json;
using System.Web;
using Orchard;
using Orchard.MediaLibrary.Services;

namespace Microsoft.CognitiveServices.Portal.GarageContent.Services
{
    public class LabApiDetailService : ILabApiDetailService
    {
        private readonly IOrchardServices orchardServices;
        private readonly IRepository<LabFeatureRecord> featureRecordRepository;
        private readonly IMediaLibraryService mediaLibraryService;
        public LabApiDetailService(IOrchardServices orchardServices, IRepository<LabFeatureRecord> featureRecordRepository, IMediaLibraryService mediaLibraryService)
        {
            this.orchardServices = orchardServices;
            this.featureRecordRepository = featureRecordRepository;
            this.mediaLibraryService = mediaLibraryService;
        }
        public LabApiDetailPartViewModel BuildEditorViewModel(LabApiDetailPart part)
        {

            if (part == null)
            {
                throw new ArgumentNullException("LabApiDetailPart");
            }

            LabApiDetailPartViewModel viewModel = new LabApiDetailPartViewModel()
            {
                Title = part.Title,
                Description = part.Description,
                DocumentationLink = part.DocumentationLink,
                Weight = part.Weight,
                ApiReferenceUrl = part.ApiReferenceUrl,
                SubscribeLink = part.SubscribeLink,
                SDKLink = part.SDKLink,
                LabApiStatus = part.LabApiStatus,
                HeaderImageUrl = string.IsNullOrEmpty(part.HeaderImageUrl) ? string.Empty : mediaLibraryService.GetMediaPublicUrl("Media", "") + part.HeaderImageUrl.Replace(mediaLibraryService.GetMediaPublicUrl("Media", ""), ""),
                GalleryImageUrl = string.IsNullOrEmpty(part.GalleryImageUrl) ? string.Empty : mediaLibraryService.GetMediaPublicUrl("Media", "") + part.HeaderImageUrl.Replace(mediaLibraryService.GetMediaPublicUrl("Media", ""), ""),
                Caption = part.Caption,
            };

            foreach (LabFeatureRecord feature in part.Features)
            {
                viewModel.Features.Add((LabFeatureRecord)feature.Clone());
            }
            return viewModel;
        }

        public void UpdatePartsFromViewModel(LabApiDetailPart part, LabApiDetailPartViewModel viewModel)
        {
            if (part == null)
            {
                throw new ArgumentNullException("LabApiDetailPart");
            }

            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            part.Title = viewModel.Title;
            part.Description = viewModel.Description;
            part.DocumentationLink = viewModel.DocumentationLink;
            part.Weight = viewModel.Weight;
            part.LabApiStatus = viewModel.LabApiStatus;
            part.ApiReferenceUrl = viewModel.ApiReferenceUrl;
            part.SDKLink = viewModel.SDKLink;
            part.SubscribeLink = viewModel.SubscribeLink;
            part.HeaderImageUrl = string.IsNullOrEmpty(viewModel.HeaderImageUrl) ? string.Empty : part.HeaderImageUrl.Replace(mediaLibraryService.GetMediaPublicUrl("Media", ""), "");
            part.GalleryImageUrl = string.IsNullOrEmpty(viewModel.GalleryImageUrl) ? string.Empty : part.GalleryImageUrl.Replace(mediaLibraryService.GetMediaPublicUrl("Media", ""), "");
            part.Caption = viewModel.Caption;

            UpdateLabApiDetailChildFields(this.featureRecordRepository, viewModel.FeaturesJson, part);
            part.Record.Features = this.featureRecordRepository.Fetch(r => r.LabApiDetailPartRecord.Id == part.Record.Id).ToList();
        }

        private void UpdateLabApiDetailChildFields<T>(
          IRepository<T> repository,
          string json,
          LabApiDetailPart part) where T : LabApiSubFieldRecord
        {
            IEnumerable<T> oldRecords = repository.Fetch(r => r.LabApiDetailPartRecord.Id == part.Id);
            IList<T> updatedRecords = JsonConvert.DeserializeObject<IList<T>>(json);

            IEnumerable<T> deletedRecords = oldRecords.Where(x => !updatedRecords.Any(u => u.Id == x.Id));

            foreach (T deletedRecord in deletedRecords)
            {
                repository.Delete(deletedRecord);
            }

            foreach (T updatedRecord in updatedRecords)
            {
                if (updatedRecord.Id <= 0)
                {
                    updatedRecord.LabApiDetailPartRecord = part.Record;
                    repository.Create(updatedRecord);
                }
                else
                {
                    updatedRecord.LabApiDetailPartRecord = part.Record;
                    repository.Update(updatedRecord);
                }
            }
        }
    }
}