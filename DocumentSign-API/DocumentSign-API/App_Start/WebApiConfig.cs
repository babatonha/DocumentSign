using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.AspNet.OData.Batch;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.Owin.Security.OAuth;
using DocumentSign_Models.ViewModels;

namespace DocumentSign_API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);


            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
         
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            config.Filter().Expand().Select().OrderBy().MaxTop(null).Count();

            builder.EntitySet<vwAccountsApproval>("vwAccountsApprovals");
            builder.EntitySet<vwAspNetRole>("vwAspNetRoles");
            builder.EntitySet<vwAspNetUser>("vwAspNetUsers");
            builder.EntitySet<vwAspNetUserRole>("vwAspNetUserRoles");
   
            builder.EntitySet<vwRequisition>("vwRequisitions");
            builder.EntitySet<vwRequisitionDocument>("vwRequisitionDocuments");
            builder.EntitySet<VwUserProfile>("vwUserProfiles");
            builder.EntitySet<vwRequisitionTemplate>("vwRequisitionTemplates");


            builder.EntitySet<VwDocument>("VwDocuments");
            builder.EntitySet<VwUserDepartment>("VwUserDepartments");
            builder.EntitySet<VwDepartment>("VwDepartments");


            config.Count().Filter().OrderBy().Expand().Select().MaxTop(null);
            var EdmModel = builder.GetEdmModel();
            config.MapODataServiceRoute("odata", null, EdmModel, new DefaultODataBatchHandler(GlobalConfiguration.DefaultServer));
            config.EnsureInitialized();
        }
    }
}
