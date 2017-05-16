using Orchard.UI.Resources;

namespace Microsoft.CognitiveServices.Portal.GarageContent
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();
            manifest.DefineStyle("LabApi-Style").SetUrl("LabApi.min.css", "LabApi.css").SetAttribute("data-timestamped", "true");
            manifest.DefineStyle("LabApi-Edit-Feature-Style").SetUrl("LabApi-Edit-Feature.css");
            manifest.DefineScript("LabApi").SetUrl("LabApi.js").SetDependencies("jQuery");
        }
    }
}