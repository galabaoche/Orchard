using Newtonsoft.Json;
using Orchard.Data.Conventions;
using System;

namespace Microsoft.CognitiveServices.Portal.GarageContent.Models
{
    public class LabFeatureRecord : LabApiSubFieldRecord, ICloneable
    {
        public virtual string Name { get; set; }

        [StringLengthMax]
        public virtual string Description { get; set; }
        public virtual int SortOrder { get; set; }
        public virtual string Category { get; set; }
        public virtual string FeatureContainer { get; set; }
        public virtual string ImageUrl { get; set; }

        public object Clone()
        {
            return new LabFeatureRecord()
            {
                Id = this.Id,
                Name = this.Name,
                Category = this.Category,
                Description = this.Description,
                FeatureContainer = this.FeatureContainer,
                SortOrder = this.SortOrder,
                ImageUrl = this.ImageUrl,
                LabApiDetailPartRecord = this.LabApiDetailPartRecord,
            };
        }
    }
}