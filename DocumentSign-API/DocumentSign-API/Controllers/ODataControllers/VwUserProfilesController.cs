using System.Data;
using System.Linq;
using System.Web.Http;
using DocumentSign_Models;
using DocumentSign_Models.ViewModels;
using Microsoft.AspNet.OData;

namespace DocumentSign_API.Controllers.ODataControllers
{

    public class VwUserProfilesController : ODataController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: odata/VwUserProfiles
        [Microsoft.AspNet.OData.EnableQuery(AllowedQueryOptions = Microsoft.AspNet.OData.Query.AllowedQueryOptions.All, MaxNodeCount = 50000)]
        public IQueryable<VwUserProfile> GetVwUserProfiles()
        {
            return db.VwUserProfiles;
        }

        // GET: odata/VwUserProfiles(5)
        [Microsoft.AspNet.OData.EnableQuery(AllowedQueryOptions = Microsoft.AspNet.OData.Query.AllowedQueryOptions.All, MaxNodeCount = 50000)]
        public SingleResult<VwUserProfile> GetVwUserProfile([FromODataUri] int key)
        {
            return SingleResult.Create(db.VwUserProfiles.Where(vwUserProfile => vwUserProfile.UserId == key));
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
