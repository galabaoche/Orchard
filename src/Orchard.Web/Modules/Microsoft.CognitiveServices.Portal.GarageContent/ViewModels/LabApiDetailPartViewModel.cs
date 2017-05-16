using Microsoft.CognitiveServices.Portal.GarageContent.Models;
using Orchard;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Microsoft.CognitiveServices.Portal.GarageContent.ViewModels
{
    public class LabApiDetailPartViewModel
    {
        public LabApiDetailPartViewModel()
        {
            Features = new List<LabFeatureRecord>();
        }

        public string Title { get; set; }
        public string HeaderImageUrl { get; set; }
        public string Description { get; set; }
        public string DocumentationLink { get; set; }
        public string SubscribeLink { get; set; }

        public string SDKLink { get; set; }

        public string FeaturesJson { get; set; }
        public IList<LabFeatureRecord> Features { get; set; }
        public int Weight { get; set; }
        public string ApiReferenceUrl { get; set; }
        public string LabApiStatus { get; set; }
        public string GalleryImageUrl { get; set; }
        public string Caption { get; set; }
        public List<SelectListItem> LabApiStatusTypes
        {
            get
            {
                return new List<SelectListItem> {
                    new SelectListItem { Text = "*None*", Value = "", Selected=true },
                    new SelectListItem { Text = "Experimental", Value = "Experimental" }
                };
            }
        }
    }
}