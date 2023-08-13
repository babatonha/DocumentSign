using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using DocumentSign_Models;
using DocumentSign_Models.ViewModels;
using Microsoft.AspNet.OData;

namespace DocumentSign_API.Controllers.ODataControllers
{
    public class VwDepartmentsController : ODataController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: odata/VwDepartments
        [Microsoft.AspNet.OData.EnableQuery(AllowedQueryOptions = Microsoft.AspNet.OData.Query.AllowedQueryOptions.All, MaxNodeCount = 50000)]
        public IQueryable<VwDepartment> GetVwDepartments()
        {
            return db.VwDepartment;
        }

        // GET: odata/VwDepartments(5)
        [Microsoft.AspNet.OData.EnableQuery(AllowedQueryOptions = Microsoft.AspNet.OData.Query.AllowedQueryOptions.All, MaxNodeCount = 50000)]
        public SingleResult<VwDepartment> GetVwDepartment([FromODataUri] int key)
        {
            return SingleResult.Create(db.VwDepartment.Where(vwDepartment => vwDepartment.DepartmentId == key));
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
