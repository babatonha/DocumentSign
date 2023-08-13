using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using DocumentSign_BusinessLayer;
using DocumentSign_Models;
using DocumentSign_Models.ViewModels;
using SHRS_BusinessLayer;

namespace DocumentSign_API.Controllers.NonODataControllers
{
    public class AccountsRequisitionsController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();
        AccountsRequisitionsRepository repo = new AccountsRequisitionsRepository();
        DocumentsRepository docRepo = new DocumentsRepository();

        // GET: api/AccountsRequisitions
        //[HttpGet]
        //public List<vwAccountsApproval> GetAccountsRequisitions(DateTime startDate, DateTime endDate)
        //{
        //    repo.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;
        //    return repo.GetAccountsRequisitions(startDate, endDate);
        //}

        //[HttpGet]
        //public HttpResponseMessage DownloadAccountsExcelFile(string requisitionIDs)
        //{
        //    docRepo.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;
        //    return docRepo.DownloadAccountsExcelFile(requisitionIDs);
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