using System.Data;
using System.Linq;
using System.Web.Http;
using DocumentSign_Models;
using DocumentSign_Models.ViewModels;
using Microsoft.AspNet.OData;

namespace DocumentSign_API.Controllers.ODataControllers
{
    public class VwUserDepartmentsController : ODataController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: odata/VwUserDepartments
        [Microsoft.AspNet.OData.EnableQuery(AllowedQueryOptions = Microsoft.AspNet.OData.Query.AllowedQueryOptions.All, MaxNodeCount = 50000)]
        public IQueryable<VwUserDepartment> GetVwUserDepartments()
        {
            return db.VwUserDepartments;
        }

        // GET: odata/VwUserDepartments(5)
        [Microsoft.AspNet.OData.EnableQuery(AllowedQueryOptions = Microsoft.AspNet.OData.Query.AllowedQueryOptions.All, MaxNodeCount = 50000)]
        public SingleResult<VwUserDepartment> GetVwUserDepartment([FromODataUri] long key)
        {
            return SingleResult.Create(db.VwUserDepartments.Where(vwUserDepartment => vwUserDepartment.UserDepartmentId == key));
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
