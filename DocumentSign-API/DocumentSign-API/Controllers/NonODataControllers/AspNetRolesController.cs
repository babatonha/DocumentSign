using System.Web.Http;
using DocumentSign_BusinessLayer;
using DocumentSign_Models;
using DocumentSign_Models.Common;
using DocumentSign_Models.TableModels;

namespace DocumentSign_API.Controllers.NonODataControllers
{
    public class AspNetRolesController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();
        //RolesRepository repo = new RolesRepository();

        //// GET: api/AspNetRoles
        //[HttpPost]
        //public ResponseObject SaveEditRole([FromBody] ModuleRole role)
        //{
        //    repo.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;
        //    return repo.SaveEditRole(role);
        //}


        //[HttpGet]
        //public ResponseObject DeleteRole(int ID)
        //{
        //    repo.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;
        //    return repo.DeleteRole(ID);
        //}
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