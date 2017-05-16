using Microsoft.CognitiveServices.Portal.GarageContent.Models;
using System;

namespace Microsoft.CognitiveServices.Portal.GarageContent.ViewModels
{
    public class LabFeatureDisplayViewModel
    {
        private readonly LabFeatureRecord featureRecord;
        public LabFeatureDisplayViewModel(LabFeatureRecord featureRecord)
        {
            this.featureRecord = featureRecord;
        }
        public string Name
        {
            get { return this.featureRecord.Name; }
        }

        public string Description
        {
            get { return this.featureRecord.Description; }
        }

        public string Category
        {
            get { return this.featureRecord.Category; }
        }

        public string FeatureContainer
        {
            get { return this.featureRecord.FeatureContainer; }
        }

        public string ImageUrl
        {
            get { return this.featureRecord.ImageUrl; }
        }
    }
}