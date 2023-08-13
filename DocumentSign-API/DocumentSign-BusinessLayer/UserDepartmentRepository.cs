using DocumentSign_Models;
using DocumentSign_Models.Common;
using DocumentSign_Models.Enums;
using DocumentSign_Models.TableModels;
using MbpmAPI.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHRS_BusinessLayer
{
   public class UserDepartmentRepository
        {
            public string connectionString = "";

        public object AccessValidator { get; private set; }

        public ResponseObject SaveUserDepartment(UserDepartment dept)
            {
                ResponseObject response = new ResponseObject();

                if (response.Status == ResponseStatus.Success)
                    {
                        using (DatabaseContext db = new DatabaseContext())
                        {
                            db.Database.Connection.ConnectionString = connectionString;
                            try
                            {
                                UserDepartment existing_dept = db.UserDepartments.Find(dept.DepartmentId);

                                if (existing_dept == null)
                                {
                                    UserDepartment existing_userDept = db.UserDepartments.Where(x => x.DepartmentId == dept.DepartmentId && x.UserId == dept.UserId).FirstOrDefault();

                                    if (existing_userDept != null)
                                    {
                                        response.Status = ResponseStatus.Failure;
                                        response.StatusDescription = "You have already saved this department.";
                                    }
                                    else
                                    {
                                        db.UserDepartments.Add(dept);
                                        db.SaveChanges();
                                        //response.EntityID = role.ID;
                                        response.Status = ResponseStatus.Success;
                                        response.StatusDescription = "Department successfully saved.";
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                response.Status = ResponseStatus.Exception;
                                response.StatusDescription = ex.Message;
                                ErrorLogger.connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                                ErrorLogger.LogError("Document Sign", "Save User Department", "Save User Department", ex);
                            }
                        }
                    }

                return response;
            }

            public ResponseObject DeleteUserDepartment(long ID)
            {
                ResponseObject response = new ResponseObject();
                if (response.Status == ResponseStatus.Success)
                {
                    using (DatabaseContext db = new DatabaseContext())
                    {
                        db.Database.Connection.ConnectionString = connectionString;
                        try
                        {
                            UserDepartment userDepartment = db.UserDepartments.Find(ID);
                            if (userDepartment == null)
                            {
                                response.Status = ResponseStatus.Failure;
                                response.StatusDescription = "File not found";
                            }
                            else
                            {
                                db.UserDepartments.Remove(userDepartment);
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
                            ErrorLogger.LogError("Document Sign", "Delete User Department", "Delete User Department", ex);
                        }
                    }
                }
                return response;
            }
        }
   
}
