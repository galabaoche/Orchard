using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using Orchard.Data.Conventions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.CognitiveServices.Portal.GarageContent.Models
{
    public class LabApiDetailPartRecord : ContentPartRecord
    {
        public LabApiDetailPartRecord()
        {
            Features = new List<LabFeatureRecord>();
        }

        public virtual string Title { get; set; }

        [StringLengthMax]
        public virtual string Description { get; set; }
        public virtual string HeaderImageUrl { get; set; }
        public virtual string DocumentationLink { get; set; }
        public virtual string ApiReferenceUrl { get; set; }
        public virtual string SubscribeLink { get; set; }

        public virtual string SDKLink { get; set; }

        public virtual IList<LabFeatureRecord> Features { get; set; }
        public virtual string LabApiStatus { get; set; }
        public virtual string GalleryImageUrl { get; set; }
        public virtual string Caption { get; set; }
        public virtual int Weight { get; set; }
    }
    public class LabApiDetailPart : ContentPart<LabApiDetailPartRecord>
    {
        [Required]
        public string Title
        {
            get { return Retrieve(x => x.Title); }
            set { Store(x => x.Title, value); }
        }

        [Required]
        public string Description
        {
            get { return Retrieve(x => x.Description); }
            set { Store(x => x.Description, value); }
        }
        public string HeaderImageUrl
        {
            get { return Retrieve(x => x.HeaderImageUrl); }
            set { Store(x => x.HeaderImageUrl, value); }
        }

        public string DocumentationLink
        {
            get { return Retrieve(x => x.DocumentationLink); }
            set { Store(x => x.DocumentationLink, value); }
        }

        public string ApiReferenceUrl
        {
            get { return Retrieve(x => x.ApiReferenceUrl); }
            set { Store(x => x.ApiReferenceUrl, value); }
        }
        public string SubscribeLink
        {
            get { return Retrieve(x => x.SubscribeLink); }
            set { Store(x => x.SubscribeLink, value); }
        }

        public string SDKLink
        {
            get { return Retrieve(x => x.SDKLink); }
            set { Store(x => x.SDKLink, value); }
        }

        public string LabApiStatus
        {
            get { return Retrieve(x => x.LabApiStatus); }
            set { Store(x => x.LabApiStatus, value); }
        }
        public string GalleryImageUrl
        {
            get { return Retrieve(x => x.GalleryImageUrl); }
            set { Store(x => x.GalleryImageUrl, value); }
        }
        public string Caption
        {
            get { return Retrieve(x => x.Caption); }
            set { Store(x => x.Caption, value); }
        }
        public int Weight
        {
            get { return Retrieve(x => x.Weight); }
            set { Store(x => x.Weight, value); }
        }
        public IList<LabFeatureRecord> Features
        {
            get { return Record.Features; }
            set { Record.Features = value; }
        }
    }
}