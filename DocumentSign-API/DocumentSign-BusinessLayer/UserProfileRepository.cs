using MbpmAPI.BusinessLogic;
using DocumentSign_Models;
using DocumentSign_Models.Common;
using DocumentSign_Models.Enums;
using DocumentSign_Models.TableModels;
using System;
using System.Configuration;
using System.Web;
using System.Linq;
using System.IO;

namespace DocumentSign_BusinessLayer
{
    public class UserProfileRepository
    {
        public string connectionString = "";

        public ResponseObject SaveEditUserProfile(UserProfile userProfile)
        {
            ResponseObject response = new ResponseObject();
            if (response.Status == ResponseStatus.Success)
            {
                using (DatabaseContext db = new DatabaseContext())
                {
                    db.Database.Connection.ConnectionString = connectionString;
                    try
                    {

                        UserProfile existing_userprofile = db.UserProfiles.Find(userProfile.UserId);

                        if (existing_userprofile == null)
                        {
                            db.UserProfiles.Add(userProfile);
                            db.SaveChanges();
                            response.EntityID = userProfile.UserId;
                            response.Status = ResponseStatus.Success;
                            response.StatusDescription = "Record successfully saved.";
                        }
                        else
                        {
                            existing_userprofile.Email = userProfile.Email;
                            existing_userprofile.Signature = userProfile.Signature;
                            existing_userprofile.PhoneNumber = userProfile.PhoneNumber;
                            existing_userprofile.FullName = userProfile.FullName;
                            existing_userprofile.IsUploadedPictureSignature = userProfile.IsUploadedPictureSignature;
                            existing_userprofile.AspNetUserId = userProfile.AspNetUserId;
                            existing_userprofile.SignatureFileType = userProfile.SignatureFileType;

                            db.SaveChanges();
                            response.EntityID = userProfile.UserId;
                            response.Status = ResponseStatus.Success;
                            response.StatusDescription = "Record successfully saved.";
                        }
                    }
                    catch (Exception ex)
                    {
                        response.Status = ResponseStatus.Exception;
                        response.StatusDescription = ex.Message;
                        ErrorLogger.connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                        ErrorLogger.LogError("Document Sign", "Save Profile", "Save Profile", ex);
                    }
                }
            }
            return response;
        }

        public ResponseObject UploadSignatureFromSignaturePad(int userId)
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
                            UserProfile attachmentFile = db.UserProfiles.Find(userId);
                            if (attachmentFile != null)
                            {
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
                                    attachmentFile.Signature = byteArray;
                                    attachmentFile.IsUploadedPictureSignature = false;

                                    db.SaveChanges();
                                    response.EntityID = attachmentFile.UserId;
                                    response.Status = ResponseStatus.Success;
                                    response.StatusDescription = "File uploaded successfully";
                                }
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
                ErrorLogger.LogError("Payment Requisition", "Upload Signature", "Save Profile Signature", ex);
            }


            return response;
        }

        public ResponseObject UploadImageSignature(int userId)
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
                            UserProfile existing_Email = db.UserProfiles.Find(userId);
                            if (existing_Email != null)
                            {
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
                                    existing_Email.IsUploadedPictureSignature = true;
                                    existing_Email.Signature = byteArray;
                                    db.SaveChanges();
                                    response.Status = ResponseStatus.Success;
                                    response.StatusDescription = "File uploaded successfully";
                                }
                            }
                            else
                            {
                                response.Status = ResponseStatus.Failure;
                                response.StatusDescription = "No profile matches the email entered";
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
                ErrorLogger.LogError("Document Sign", "Upload Picture Signature", "Upload picture signature", ex);
            }

            return response;
        }

    }
}
