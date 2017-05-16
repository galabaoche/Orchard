using Microsoft.CognitiveServices.Portal.GarageContent.Models;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Common.Fields;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Fields.Fields;
using Orchard.Indexing;
using Orchard.MediaLibrary.Fields;

namespace Microsoft.CognitiveServices.Portal.GarageContent
{
    public class Migrations : DataMigrationImpl
    {
        public int Create()
        {
            #region Create Lab Service Detail and Feature Records

            SchemaBuilder.CreateTable("LabFeatureRecord", table => table
                 .Column<int>("Id", column => column.PrimaryKey().Identity())
                 .Column<string>("Name")
                 .Column<string>("Description", column => column.Unlimited())
                 .Column<int>("SortOrder")
                 .Column<string>("Category")
                 .Column<string>("FeatureContainer")
                 .Column<string>("ImageUrl")
                 .Column<int>("LabApiDetailPartRecord_Id")
                );

            SchemaBuilder.CreateTable("LabApiDetailPartRecord", table => table
                .ContentPartRecord()
                .Column<string>("Title")
                .Column<string>("HeaderImageUrl")
                .Column<string>("Description", column => column.Unlimited())
                .Column<string>("DocumentationLink")
                .Column<string>("ApiReferenceUrl")
                .Column<string>("SubscribeLink")
                .Column<string>("LabApiStatus")
                .Column<string>("GalleryImageUrl")
                .Column<string>("Caption")
                .Column<int>("Weight"));

            ContentDefinitionManager.AlterPartDefinition(
                typeof(LabApiDetailPart).Name, cfg => cfg
                .Attachable()
                .WithDescription("Lab Api detail part"));

            ContentDefinitionManager.AlterTypeDefinition(
                "LabApiDetailPage", cfg => cfg
                 .WithPart(typeof(LabApiDetailPart).Name)
                 .WithPart("BodyPart")
                 .WithPart("CommonPart", p => p
                         .WithSetting("DateEditorSettings.ShowDateEditor", "False"))
                 .Draftable()
                 .Listable()
                 .Securable()
                 .Indexed()
                 .Creatable()
                 .WithPart("LocalizationPart")
                 .WithPart("AutoroutePart", builder => builder
                         .WithSetting("AutorouteSettings.AllowCustomPattern", "True")
                         .WithSetting("AutorouteSettings.AutomaticAdjustmentOnEdit", "False")
                         .WithSetting("AutorouteSettings.PatternDefinitions", "[{\"Name\":\"lab Api title\",\"Pattern\":\"{Content.Slug}\",\"Description\":\"lab Apis\"}]")
                         .WithSetting("AutorouteSettings.DefaultPatternIndex", "0")));
            #endregion

            return 1;
        }

        public int UpdateFrom1()
        {
            ContentDefinitionManager.AlterPartDefinition(
               typeof(LabApiGalleryPart).Name, builder => builder
               .WithDescription("lab Api Gallery")
               .WithField("HeaderImageUrl", cfg => cfg
                   .OfType(typeof(MediaLibraryPickerField).Name)
                   .WithDisplayName("Header Image Url")
                   .WithSetting("MediaLibraryPickerFieldSettings.AllowedExtensions", "jpg png")
                   .WithSetting("MediaLibraryPickerFieldSettings.Hint", "This will be the lab api gallery header image.")
                   .WithSetting("MediaLibraryPickerFieldSettings.Required", "True")
                   .WithSetting("MediaLibraryPickerFieldSettings.Multiple", "False"))

               .WithField("Title", cfg => cfg
                      .OfType(typeof(TextField).Name)
                      .WithDisplayName("Title")
                      .WithSetting("TextFieldSettings.Hint", "Enter the name of the Title.")
                      .WithSetting("TextFieldSettings.Required", "True"))

               .WithField("Description", cfg => cfg
                   .OfType(typeof(TextField).Name)
                   .WithDisplayName("Description")
                   .WithSetting("TextFieldSettings.Flavor", "textarea")
                   .WithSetting("TextFieldSettings.Hint", "Enter the description of the lab Api Gallery")
                   .WithSetting("TextFieldSettings.Required", "True"))

               .WithField("LabTermsLink", cfg => cfg
                   .OfType(typeof(LinkField).Name)
                   .WithDisplayName("Labs Terms Link")
                   .WithSetting("LinkFieldSettings.TargetMode", "NewWindow"))

               .WithField("FAQName", cfg => cfg
                      .OfType(typeof(TextField).Name)
                      .WithDisplayName("FAQName")
                      .WithSetting("TextFieldSettings.Hint", "Enter the name of the FAQ Name.")
                      .WithSetting("TextFieldSettings.Required", "True"))
            );

            ContentDefinitionManager.AlterTypeDefinition(
                 "LabApiGalleryPage", builder => builder
                 .DisplayedAs("Lab Api Gallery Page")
                 .WithPart(typeof(LabApiGalleryPart).Name)
                 .WithPart("CommonPart", p => p.WithSetting("DateEditorSettings.ShowDateEditor", "False"))
                 .WithPart("LocalizationPart")
                 .WithPart("BodyPart")
                 .Draftable()
                 .Listable()
                 .Securable()
                 .Indexed()
                 .Creatable()
                 .WithPart("AutoroutePart", cfg => cfg
                     .WithSetting("AutorouteSettings.AllowCustomPattern", "True")
                     .WithSetting("AutorouteSettings.AutomaticAdjustmentOnEdit", "False")
                     .WithSetting("AutorouteSettings.PatternDefinitions", "[{Name:'Lab Api Gallery', Pattern: '{Content.Slug}', Description: 'Lab Api Gallery'}]")
                     .WithSetting("AutorouteSettings.DefaultPatternIndex", "0"))
            );
            return 2;
        }

        public int UpdateFrom2()
        {
            SchemaBuilder.AlterTable("LabApiDetailPartRecord", table => table
                .AddColumn<string>("SDKLink"));

            return 3;
        }
    }
}