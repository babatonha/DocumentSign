using ClosedXML.Excel;
using FastMember;
using MbpmAPI.BusinessLogic;
using DocumentSign_Models;
using DocumentSign_Models.Common;
using DocumentSign_Models.Enums;
using DocumentSign_Models.TableModels;
using DocumentSign_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;


namespace DocumentSign_BusinessLayer
{
    public class DocumentsRepository
    {
        public string connectionString = "";


        public ResponseObject UploadDocument(int userId)
        {
            ResponseObject response = new ResponseObject();

            try
            {
                if (response.Status == ResponseStatus.Success)
                {
                    byte[] byteArray;

                    using (DatabaseContext db = new DatabaseContext())
                    {
                        db.Database.Connection.ConnectionString = connectionString;

                        var httpRequest = HttpContext.Current.Request;

                        foreach (string file in httpRequest.Files)
                        {
                            Document attachmentFile = new Document();

                            var postedFile = httpRequest.Files[file];
                            BinaryReader binReader = new BinaryReader(postedFile.InputStream);
                            byteArray = binReader.ReadBytes(postedFile.ContentLength);

                            if (byteArray == null)
                            {
                                response.Status = ResponseStatus.Failure;
                                response.StatusDescription = "No file has been selected";
                            }
                            else
                            { 
                                attachmentFile.UploadedBy = userId;
                                attachmentFile.DateUploaded = DateTime.Now;
                                attachmentFile.LocationOriginal = byteArray;
                                attachmentFile.DocumentName = postedFile.FileName;

                                db.Documents.Add(attachmentFile);
                                db.SaveChanges();
                                response.EntityID = attachmentFile.DocumentId;
                                response.Status = ResponseStatus.Success;
                                response.StatusDescription = "File uploaded successfully";
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Exception;
                response.StatusDescription = ex.Message;

                ErrorLogger.connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                ErrorLogger.LogError("Document Sign", "Upload Document", "Upload Document", ex);
            }


            return response;
        }

        public HttpResponseMessage DownloadDocument(string fileID)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseObject response = new ResponseObject();

            try
            {
                if (response.Status == ResponseStatus.Success)
                {

                    using (DatabaseContext db = new DatabaseContext())
                    {
                        db.Database.Connection.ConnectionString = connectionString;

                        int downloadFileID = int.Parse(fileID);

                        Document file = db.Documents.Find(downloadFileID);

                        var fileBytes = file.LocationOriginal;

                        var fileMemStream = new MemoryStream(fileBytes);

                        result.Content = new StreamContent(fileMemStream);

                        var headers = result.Content.Headers;

                        headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                        headers.ContentDisposition.FileName = file.DocumentName;
                        headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                        headers.ContentLength = fileMemStream.Length;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Exception;
                response.StatusDescription = ex.Message;

                ErrorLogger.connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                ErrorLogger.LogError("Document System", "Download Document", "Download Document", ex);
            }
            return result;
        }

        public ResponseObject DeleteDocument(long ID)
        {
            ResponseObject response = new ResponseObject();
            if (response.Status == ResponseStatus.Success)
            {
                using (DatabaseContext db = new DatabaseContext())
                {
                    db.Database.Connection.ConnectionString = connectionString;
                    try
                    {
                        //check if the record exists
                        Document doc = db.Documents.Find(ID);

                        if (doc != null)
                        {
                            db.Documents.Remove(doc);
                            db.SaveChanges();
                            response.Status = ResponseStatus.Success;
                            response.StatusDescription = "Successfully deleted.";
                        }
                        else
                        {
                            response.Status = ResponseStatus.Failure;
                            response.StatusDescription = "Delete failed.";
                        }
                    }
                    catch (Exception ex)
                    {
                        response.Status = ResponseStatus.Exception;
                        response.StatusDescription = ex.Message;
                        ErrorLogger.connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                        ErrorLogger.LogError("Document Sign", "Delete Document", "Delete Document", ex);
                    }
                }
            }
            return response;
        }
    }
}
