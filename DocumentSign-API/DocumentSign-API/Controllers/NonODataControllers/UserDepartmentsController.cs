using DocumentSign_Models;
using DocumentSign_Models.Common;
using DocumentSign_Models.TableModels;
using SHRS_BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DocumentSign_API.Controllers.NonODataControllers
{
    public class UserDepartmentsController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();
        UserDepartmentRepository repo = new UserDepartmentRepository();

        // GET: api/UserDepartments
        [HttpPost]
        public ResponseObject SaveUserDepartment([FromBody] UserDepartment dept)
        {
            repo.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;
            return repo.SaveUserDepartment(dept);
        }

        [HttpGet]
        public ResponseObject DeleteUserDepartment(long ID)
        {
            repo.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;
            return repo.DeleteUserDepartment(ID);
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
