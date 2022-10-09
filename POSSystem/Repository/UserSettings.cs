using POSSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace POSSystem.Repository
{
    public class UserSettings
    {
        public string Action { get; set; }
        public DataSet GetUserNotifications(string userId)
        {
            DataSet result = new DataSet();

            try
            {
                string query = "dbo.GetUserNotifications";

                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("UserId", userId)
                };

                DataAccessHelper db = new DataAccessHelper();
                result = db.GetData(query, parameters, System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        public EmployeeDetails GetEmployeeDetails(string userId)
        {
            string query = "[dbo].[GetUserInformation]";
            string[] paramValue = new string[] { "@UserId" };
            string[] paramName = new string[] { userId };

            DataAccessHelper db = new DataAccessHelper();
            return db.GetData<EmployeeDetails>(query, paramValue, paramName, CommandType.StoredProcedure)[0];
        }

        public DataSet ViewProfile(string Id,string userid)
        {
            DataSet result = new DataSet();
            try
            {
                string query = "[dbo].[ViewUserProfile]";

                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@Id",Id),
                    new SqlParameter("@UserId",userid)
                };

                DataAccessHelper db = new DataAccessHelper();
                result = db.GetData(query, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        public DataSet AuthenticateLogIn(string userId, string password)
        {
            DataSet result = new DataSet();
            try
            {
                string query = "[dbo].[AuthenticateUser]";

                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@UserId",userId),
                    new SqlParameter("@Password",password)
                };

                DataAccessHelper db = new DataAccessHelper();
                result = db.GetData(query, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        public TransactionResult RegisterUser(UserRegistration model)
        {
            TransactionResult result = new TransactionResult();
            try
            {

                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@UserId",model.UserId),
                    new SqlParameter("@EmailAddress",model.EmailAddress),
                    new SqlParameter("@ConfirmEmailAddress",model.ConfirmEmailAddress),
                    new SqlParameter("@Password",model.Password),
                    new SqlParameter("@ConfirmPassword",model.ConfirmPassword),
                    new SqlParameter("@FirstNm",model.FirstNm),
                    new SqlParameter("@LastNm",model.LastNm)
                };

                DataAccessHelper db = new DataAccessHelper();
                result = db.GetData<TransactionResult>("[dbo].[RegisterUser]", parameters, CommandType.StoredProcedure)[0];
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        public TransactionResult SendMessage(string message,string sender,string receiver)
        {
            TransactionResult result = new TransactionResult();
            try
            {

                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@param1",message),
                    new SqlParameter("@param2",sender),
                    new SqlParameter("@param3",receiver)
                };

                DataAccessHelper db = new DataAccessHelper();
                result = db.GetData<TransactionResult>("[dbo].[sp_SendMessage]", parameters, CommandType.StoredProcedure)[0];
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        public DataSet UserAccountList(string userId)
        {
            DataSet result = new DataSet();
            try
            {
                string query = "[dbo].[GetUserAccountList]";

                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@UserId",userId)
                };

                DataAccessHelper db = new DataAccessHelper();
                result = db.GetData(query, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }

            return result;
        }
       
        public List<CodeDecode> CodeDecodeList(string param1)
        {
            List<CodeDecode> resList = new List<CodeDecode>();
            try
            {

                string query = "[dbo].[CodeDecode]";
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@param1",param1)
                };

                DataAccessHelper db = new DataAccessHelper();
                resList = db.GetData<CodeDecode>(query, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }

            return resList;
        }

        public TransactionResult UpdateUserProfile(EmployeeDetails model,string currUser)
        {
            TransactionResult result = new TransactionResult();

            try
            { 

                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("Id", model.Id),
                    new SqlParameter("FirstNm", model.FirstNm),
                    new SqlParameter("LastNm", model.LastNm),
                    new SqlParameter("MiddleNm", model.MiddleNm),
                    new SqlParameter("PhoneNumber", model.PhoneNumber),
                    new SqlParameter("Email", model.Email),
                    new SqlParameter("Role", model.Role),
                    new SqlParameter("IsActive", model.IsActive),
                    new SqlParameter("currUser",currUser)
                };

                DataAccessHelper db = new DataAccessHelper();
                result = db.GetData<TransactionResult>("dbo.UpdateUserProfile", parameters, CommandType.StoredProcedure)[0];
            }
            catch (Exception ex)
            {
                // Log exception here
                //LoggingRepository logRepo = new LoggingRepository();
                //string auditId = logRepo.LogError(model, details, attachments, model.RequestorId, "General Expense Reimbursement", ex.StackTrace, ex.GetBaseException().Message);
                //result.Code = Constants.ERROR_FLAG;
                //result.Message = "An error has encountered during the process. Kindly file a ticket in AskICT with this reference number: " + auditId;
            }

            return result;
        }
        public DataSet LogSession(string userId,string status)
        {
            DataSet result = new DataSet();
            try
            {
                string query = "[dbo].[LogSession]";

                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@userId",userId),
                    new SqlParameter("@Status",status)
                };

                DataAccessHelper db = new DataAccessHelper();
                result = db.GetData(query, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public CodeDecode IsAdminAccess(string userId)
        {
            if (userId == null) {
                userId = "";
            }
            string query = "[dbo].[CheckAdminAccess]";
            string[] paramValue = new string[] { "@UserId" };
            string[] paramName = new string[] { userId };

            DataAccessHelper db = new DataAccessHelper();
            return db.GetData<CodeDecode>(query, paramValue, paramName, CommandType.StoredProcedure)[0];
        }

    }
}