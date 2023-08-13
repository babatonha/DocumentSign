using MbpmAPI.BusinessLogic;
using DocumentSign_Models;
using DocumentSign_Models.Common;
using DocumentSign_Models.Enums;
using DocumentSign_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SHRS_BusinessLayer
{
    public class AccountsRequisitionsRepository
    {
        public string connectionString = "";

        public List<vwAccountsApproval> GetAccountsRequisitions(DateTime startDate, DateTime endDate)
        {
            ResponseObject response = new ResponseObject();
            List<vwAccountsApproval> requisitionList = new List<vwAccountsApproval>();
            try
            {
                if (response.Status == ResponseStatus.Success)
                {
                    using (DatabaseContext db = new DatabaseContext())
                    {
                        db.Database.Connection.ConnectionString = connectionString;

                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            DataTable dt = new DataTable();

                           
                            string procedureName = "spGetAccountsRequisitions '" + startDate + "'," + "'" + endDate + "'";
                            using (SqlCommand command = new SqlCommand(procedureName, con))
                            {
                                try
                                {
                                    command.CommandType = CommandType.Text;
                                    command.CommandTimeout = 0;
                                    con.Open();

                                    SqlDataAdapter da = new SqlDataAdapter(command);
                                    da.Fill(dt);

                                    if (dt.Rows.Count == 0)
                                    {
                                        response.Status = ResponseStatus.Failure;
                                        response.StatusDescription = "No data to return";
                                    }
                                    else
                                    {
                                        foreach (DataRow dr in dt.Rows)
                                        {
                                            vwAccountsApproval accountsRequisiion = new vwAccountsApproval();

                                            accountsRequisiion.ID = Convert.ToInt64(dr["ID"]);
                                            accountsRequisiion.RequisitionID = Convert.ToInt64(dr["RequisitionID"]);
                                            accountsRequisiion.RejectionReason = Convert.ToString(dr["RejectionReason"]);

                                            accountsRequisiion.ApprovedAccountsID = Convert.ToString(dr["ApprovedAccountsID"]);
                                            accountsRequisiion.ApprovedManagerID = Convert.ToString(dr["ApprovedManagerID"]);
                                            accountsRequisiion.RequisitionDate = Convert.ToDateTime(dr["RequisitionDate"]);
                                            accountsRequisiion.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                                            accountsRequisiion.PaymentToID = Convert.ToInt32(dr["PaymentToID"]);
                                            accountsRequisiion.PayeeID = Convert.ToString(dr["PayeeID"]);
                                            accountsRequisiion.InvoiceNumbers = Convert.ToString(dr["InvoiceNumbers"]);
                                            accountsRequisiion.OrderNumber = Convert.ToString(dr["OrderNumber"]);
                                            accountsRequisiion.OrderComplete = Convert.ToString(dr["OrderComplete"]);
                                            accountsRequisiion.Forecast = Convert.ToString(dr["Forecast"]);
                                            accountsRequisiion.Amount = Convert.ToDouble(dr["Amount"]);
                                            accountsRequisiion.PaymentDescription = Convert.ToString(dr["PaymentDescription"]);
                                            accountsRequisiion.POPEmail = Convert.ToString(dr["POPEmail"]);
                                            accountsRequisiion.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);
                                            accountsRequisiion.PaymentDate = Convert.ToDateTime(dr["PaymentDate"]);
                                            accountsRequisiion.Status = Convert.ToString(dr["Status"]);
                                            accountsRequisiion.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                                            accountsRequisiion.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);
                                            accountsRequisiion.ApprovalManagerName = Convert.ToString(dr["ApprovalManagerName"]);
                                            accountsRequisiion.DepartmentName = Convert.ToString(dr["DepartmentName"]);
                                            accountsRequisiion.PayeeName = Convert.ToString(dr["PayeeName"]);
                                            accountsRequisiion.RowNum = Convert.ToInt64(dr["RowNum"]);


                                            requisitionList.Add(accountsRequisiion);
                                        }
                                    }
                                    command.ExecuteNonQuery();
                                    con.Close();

                                    response.Status = ResponseStatus.Success;
                                    response.StatusDescription = "ok";
                                }
                                catch (Exception ex)
                                {
                                    response.Status = ResponseStatus.Exception;
                                    response.StatusDescription = ex.Message;
                                    requisitionList = null;
                                    ErrorLogger.connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                                    ErrorLogger.LogError("Payment Requisition", "Get Accounts Requisitions", "Accounts", ex);
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
                requisitionList = null;
                ErrorLogger.connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                ErrorLogger.LogError("Payment Requisition", "Get Accounts Requisitions", "Accounts", ex);
            }

            return requisitionList;
        }
    }
}
