using Orchard;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.Localization.Services;
using Orchard.Roles.Models;
using Orchard.UI.Navigation;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.CognitiveServices.Portal.GarageContent
{
    public class AdminMenu : INavigationProvider
    {
        private readonly IOrchardServices orchardServices;
        private readonly ICultureFilter cultureFilter;

        public AdminMenu(IOrchardServices orchardServices, ICultureFilter cultureFilter)
        {
            this.orchardServices = orchardServices;
            this.cultureFilter = cultureFilter;
        }

        public string MenuName
        {
            get
            {
                return "admin";
            }
        }

        public Localizer T { get; set; }

        public void GetNavigation(NavigationBuilder builder)
        {
            IList<string> userRoles = orchardServices.WorkContext.CurrentUser.As<UserRolesPart>().Roles;
            bool isAuthor = userRoles != null && userRoles.Any(r => string.Equals("Author", r));
            ContentItem homePage = cultureFilter.FilterCulture(orchardServices.ContentManager.Query("HomePage"), orchardServices.WorkContext.CurrentCulture).List().FirstOrDefault();

            builder.Add(T("View"), "-1", menu =>
            {
                menu.LinkToFirstChild(false);
                if (homePage != null && !isAuthor)
                {
                    menu.Add(T("Lab Api Detail Page"), "2", item => item.Action("List", "Admin", new { area = "Contents", id = "LabApiDetailPage" }));
                    menu.Add(T("Lab Api Gallery Page"), "2", item => item.Action("List", "Admin", new { area = "Contents", id = "LabApiGalleryPage" }));
                }
            });
        }
    }
}