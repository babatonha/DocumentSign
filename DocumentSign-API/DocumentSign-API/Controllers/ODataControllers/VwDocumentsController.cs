using System.Data;
using System.Linq;
using System.Web.Http;
using DocumentSign_Models;
using DocumentSign_Models.ViewModels;
using Microsoft.AspNet.OData;

namespace DocumentSign_API.Controllers.ODataControllers
{

    public class VwDocumentsController : ODataController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: odata/VwDocuments
        [Microsoft.AspNet.OData.EnableQuery(AllowedQueryOptions = Microsoft.AspNet.OData.Query.AllowedQueryOptions.All, MaxNodeCount = 50000)]
        public IQueryable<VwDocument> GetVwDocuments()
        {
            return db.VwDocuments;
        }

        // GET: odata/VwDocuments(5)
        [Microsoft.AspNet.OData.EnableQuery(AllowedQueryOptions = Microsoft.AspNet.OData.Query.AllowedQueryOptions.All, MaxNodeCount = 50000)]
        public SingleResult<VwDocument> GetVwDocument([FromODataUri] long key)
        {
            return SingleResult.Create(db.VwDocuments.Where(vwDocument => vwDocument.DocumentId == key));
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
