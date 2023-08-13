using System.Web.Http;
using DocumentSign_BusinessLayer;
using DocumentSign_Models;
using DocumentSign_Models.Common;
using DocumentSign_Models.TableModels;

namespace DocumentSign_API.Controllers.NonODataControllers
{
    public class AspNetUserRolesController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();
        UserRoleRepository repo = new UserRoleRepository();

        // GET: api/AspNetRoles
        [HttpPost]
        public ResponseObject SaveEditUserRole([FromBody] ModuleUserRole role)
        {
            repo.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;
            return repo.SaveEditUserRole(role);
        }


        [HttpGet]
        public ResponseObject DeleteUserRole(long ID)
        {
            repo.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;
            return repo.DeleteUserRole(ID);
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