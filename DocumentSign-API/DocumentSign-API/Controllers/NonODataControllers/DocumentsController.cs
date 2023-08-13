using DocumentSign_BusinessLayer;
using DocumentSign_Models.Common;
using System.Net.Http;
using System.Web.Http;

namespace DocumentSign_API.Controllers.NonODataControllers
{
    public class DocumentsController : ApiController
    {
        DocumentsRepository repo = new DocumentsRepository();

        [HttpPost]
        public ResponseObject UploadDocument(int userId)
        {
            repo.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;
            return repo.UploadDocument(userId);
        }

        [HttpGet]
        public HttpResponseMessage DownloadDocument(string fileID)
        {
            repo.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;
            return repo.DownloadDocument(fileID);
        }

        [HttpGet]
        public ResponseObject DeleteDocument(long documentId)
        {
            repo.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;
            return repo.DeleteDocument(documentId);
        }
    }
}
