using MbpmAPI.BusinessLogic;
using DocumentSign_Models;
using DocumentSign_Models.Common;
using DocumentSign_Models.Enums;
using DocumentSign_Models.TableModels;
using System;
using System.Configuration;
using System.Linq;

namespace DocumentSign_BusinessLayer
{
    public class UserRoleRepository
    {
        public string connectionString = "";

        public ResponseObject SaveEditUserRole(ModuleUserRole role)
        {
            ResponseObject response = new ResponseObject();
            if (response.Status == ResponseStatus.Success)
            {
                using (DatabaseContext db = new DatabaseContext())
                {
                    db.Database.Connection.ConnectionString = connectionString;
                    try
                    {

                        //ModuleUserRole existing_role = db.ModuleUserRoles.Where(x => x.UserId == role.UserId).FirstOrDefault();

                        //if (existing_role == null)
                        //{
                            ModuleUserRole validationRole = db.ModuleUserRoles.Where(x => x.UserId == role.UserId && x.RoleId ==role.RoleId).FirstOrDefault();
                            if(validationRole != null)
                            {
                                response.Status = ResponseStatus.Failure;
                                response.StatusDescription = "User already linked to the role.";
                            }
                            else
                            {
                                db.ModuleUserRoles.Add(role);
                                db.SaveChanges();
                                //response.EntityID = role.ID;
                                response.Status = ResponseStatus.Success;
                                response.StatusDescription = "Record successfully saved.";
                            }
                    //}
                    //    else
                    //{
                    //    existing_role.UserId = role.UserId;
                    //    existing_role.RoleId = role.RoleId;

                    //    //existing_role.AspNetRole = role.AspNetRole;
                    //    db.SaveChanges();
                    //    // response.EntityID = role.Id;
                    //    response.Status = ResponseStatus.Success;
                    //    response.StatusDescription = "Record successfully saved.";
                    //}
                }
                    catch (Exception ex)
                    {
                        response.Status = ResponseStatus.Exception;
                        response.StatusDescription = ex.Message;
                        ErrorLogger.connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                        ErrorLogger.LogError("Payment Requisition", "Save Role", "Save Role", ex);
                    }
                }
            }
            return response;
        }

        public ResponseObject DeleteUserRole(long ID)
        {
            ResponseObject response = new ResponseObject();
            if (response.Status == ResponseStatus.Success)
            {
                using (DatabaseContext db = new DatabaseContext())
                {
                    db.Database.Connection.ConnectionString = connectionString;
                    try
                    {
                        ModuleUserRole userRole = db.ModuleUserRoles.Find(ID);
                        if (userRole == null)
                        {
                            response.Status = ResponseStatus.Failure;
                            response.StatusDescription = "File not found";
                        }
                        else
                        {
                            db.ModuleUserRoles.Remove(userRole);
                            db.SaveChanges();
                            response.Status = ResponseStatus.Success;
                            response.StatusDescription = "Deleted successfully!";
                        }

                    }
                    catch (Exception ex)
                    {
                        response.Status = ResponseStatus.Exception;
                        response.StatusDescription = ex.Message;
                        ErrorLogger.connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                        ErrorLogger.LogError("Payment Requisition", "Delete Role", "Delete Role", ex);
                    }
                }
            }
            return response;
        }
    }
}
