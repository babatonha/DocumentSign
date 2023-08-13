using System.Collections.Generic;
using System.Web.Http;
using DocumentSign_BusinessLayer;
using DocumentSign_Models;
using DocumentSign_Models.Common;
using DocumentSign_Models.TableModels;

namespace DocumentSign_API.Controllers.NonODataControllers
{
    public class UserProfilesController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        UserProfileRepository repo = new UserProfileRepository();

        // GET: api/UserProfiles
        [HttpPost]
        public ResponseObject SaveEditUserProfile([FromBody] UserProfile userProfile)
        {
            repo.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;
            return repo.SaveEditUserProfile(userProfile);
        }

        [HttpPost]
        public ResponseObject UploadSignatureFromSignaturePad(int userId)
        {
            repo.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;
            return repo.UploadSignatureFromSignaturePad(userId);
        }

        [HttpPost]
        public ResponseObject UploadImageSignature(int x,int userId)
        {
            repo.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;
            return repo.UploadImageSignature(userId);
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