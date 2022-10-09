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
    public class InventorySettings
    {
        public string Action { get; set; }
        
        public DataSet GetInventoryDetails(string param1,string param2)
        {
            DataSet result = new DataSet();
            try
            {
                string query = "[dbo].[GetInventoryDetails]";

                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@param1",param1),
                    new SqlParameter("@param2",param2)
                };

                DataAccessHelper db = new DataAccessHelper();
                result = db.GetData(query, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        public DataSet GetOrderReceiving(string userid)
        {
            DataSet result = new DataSet();
            try
            {
                string query = "[dbo].[GetOrderReceiving]";

                SqlParameter[] parameters = new SqlParameter[] { 
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

        public DataSet GetOrderApproval(string userId)
        {
            DataSet result = new DataSet();
            try
            {
                string query = "[dbo].[GetOrderApprovalList]";

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

         
        public DataSet SubmittedList(string userId)
        {
            DataSet result = new DataSet();
            try
            {
                string query = "[dbo].[GetSubmittedList]";

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
        public List<Batch> GetVendorList()
        {
            List<Batch> resList = new List<Batch>();
            try
            {

                string query = "[dbo].[GetVendorList]";
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@param1","")
                };

                DataAccessHelper db = new DataAccessHelper();
                resList = db.GetData<Batch>(query, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }

            return resList;
        }
          
        public TransactionResult PostOrderReceiving(Batch model,EmployeeDetails model2, List<Product> modellist,string userId)
        {
            TransactionResult result = new TransactionResult();
            try
            {
                DataTable dtDetails = BuildProductTable(modellist);

                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@BatchNbr",model.BatchNbr),
                    new SqlParameter("@PaidAmt",model.PaidAmt.Replace(",", "")),
                    new SqlParameter("@VendorCd",model.VendorCd),
                    new SqlParameter("@VendorNm",model.VendorNm),
                    new SqlParameter("@VendorLocation",model.VendorLocation),
                    new SqlParameter("@vendorContactPerson",model.vendorContactPerson),
                    new SqlParameter("@VendorContactNbr",model.VendorContactNbr),
                    new SqlParameter("@FullNm",model2.FullNm),
                    new SqlParameter("@Role",model2.Role),
                    new SqlParameter("@UserId",userId),
                    new SqlParameter("@ProductDetails", dtDetails)

                };

                DataAccessHelper db = new DataAccessHelper();
                result = db.GetData<TransactionResult>("[dbo].[sp_PostOrderReceiving]", parameters, CommandType.StoredProcedure)[0];
            }
            catch (Exception ex)
            {
                result.Code = "0";
                result.Message = ex.Message;
            }

            return result;
        }
        
        public TransactionResult PostApprovalForm(string Action, string userId,string batchNbr)
        {
            TransactionResult result = new TransactionResult();
            try
            {  
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@param1",Action),
                    new SqlParameter("@param2",userId),
                    new SqlParameter("@param3",batchNbr)

                };

                DataAccessHelper db = new DataAccessHelper();
                result = db.GetData<TransactionResult>("[dbo].[sp_PostApprovalForm]", parameters, CommandType.StoredProcedure)[0];
            }
            catch (Exception ex)
            {
                result.Code = "0";
                result.Message = ex.Message;
            }

            return result;
        }
        private DataTable BuildProductTable(List<Product> list)
        {
            if (list == null)
            {
                list = new List<Product>();
            }

            DataTable dtResult = new DataTable();
            DataRow dr;

            dtResult.Columns.Add("ItemId");
            dtResult.Columns.Add("VariantNbr");
            dtResult.Columns.Add("ProductDesc1");
            dtResult.Columns.Add("ProductDesc2");
            dtResult.Columns.Add("ProductDesc3");
            dtResult.Columns.Add("UOM");
            dtResult.Columns.Add("ProductCategory");
            dtResult.Columns.Add("ReceivedQty");
            dtResult.Columns.Add("BatchListPrice");
            dtResult.Columns.Add("ExpiryDate");
            dtResult.Columns.Add("WithVat");

            foreach (var item in list)
            {
                dr = dtResult.NewRow();
                dr["ItemId"] = item.ItemId;
                dr["VariantNbr"] = item.VariantNbr;
                dr["ProductDesc1"] = item.ProductDesc1;
                dr["ProductDesc2"] = item.ProductDesc2;
                dr["ProductDesc3"] = item.ProductDesc3;
                dr["UOM"] = item.UOM;
                dr["ProductCategory"] = item.ProductCategory;
                dr["ReceivedQty"] = item.ReceivedQty.Replace(",", "") ;
                dr["BatchListPrice"] = item.BatchListPrice.Replace(",", ""); 
                dr["ExpiryDate"] = item.ExpiryDate;
                dr["WithVat"] = item.WithVat;

                dtResult.Rows.Add(dr);
            }

            return dtResult;
        }
        public List<CodeDecode> GetProdCategoryList()
        {
            List<CodeDecode> resList = new List<CodeDecode>();
            try
            {

                string query = "[dbo].[FilterProductCategories]";
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@param1","")
                };

                DataAccessHelper db = new DataAccessHelper();
                resList = db.GetData<CodeDecode>(query, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }

            return resList;
        }

        public List<Product> FilterProductDetails(string param1, string param2)
        {
            List<Product> resList = new List<Product>();
            try
            {
                string query = "[dbo].[FilterProductDetails]";
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@param1", param1),
                    new SqlParameter("@param2", param2)
                };

                DataAccessHelper db = new DataAccessHelper();
                resList = db.GetData<Product>(query, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }

            return resList;
        }



    }
}