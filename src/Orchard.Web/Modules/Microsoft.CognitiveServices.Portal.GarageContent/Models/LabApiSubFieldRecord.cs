using Newtonsoft.Json;

namespace Microsoft.CognitiveServices.Portal.GarageContent.Models
{
    public abstract class LabApiSubFieldRecord
    {
        public virtual int Id { get; set; }

        [JsonIgnore]
        public virtual LabApiDetailPartRecord LabApiDetailPartRecord { get; set; }
    }
}