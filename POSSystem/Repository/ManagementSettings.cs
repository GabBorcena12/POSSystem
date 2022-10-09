using POSSystem;
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
    public class ManagementSettings
    {
        public string Action { get; set; }
         
        public DataSet GetCategoryList(string userId)
        {
            DataSet result = new DataSet();
            try
            {
                string query = "[dbo].[sp_listCategoryManagement]";

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

        public TransactionResult PostCategoryManagement(Category model, string userid)
        {
            TransactionResult result = new TransactionResult();
            try
            {

                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@CategoryDesc",model.CategoryDesc),
                    new SqlParameter("@CategoryDesc2",model.CategoryDesc2),
                    new SqlParameter("@IsActive",model.IsActive),
                    new SqlParameter("@userid",userid)
                };

                DataAccessHelper db = new DataAccessHelper();
                result = db.GetData<TransactionResult>("[dbo].[sp_PostCategoryManagement]", parameters, CommandType.StoredProcedure)[0];
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public TransactionResult PostProductMaintenance(Product model, string userid, string button)
        {
            TransactionResult result = new TransactionResult();
            try
            {

                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@ItemId",model.ItemId),
                    new SqlParameter("@VariantNbr",model.VariantNbr),
                    new SqlParameter("@ProductDesc1",model.ProductDesc1),
                    new SqlParameter("@ProductDesc2",model.ProductDesc2),
                    new SqlParameter("@ProductDesc3",model.ProductDesc3),
                    new SqlParameter("@VendorCd",model.VendorCd),
                    new SqlParameter("@ProductCategory",model.ProductCategory),
                    new SqlParameter("@UOM",model.UOM),
                    new SqlParameter("@NetIncomeIdentifier",model.NetIncomeIdentifier),
                    new SqlParameter("@NetIncome",model.NetIncome),
                    new SqlParameter("@userid",userid),
                    new SqlParameter("@Type",button)
                };

                DataAccessHelper db = new DataAccessHelper();
                result = db.GetData<TransactionResult>("[dbo].[sp_PostProductMaintenance]", parameters, CommandType.StoredProcedure)[0];
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        
        public TransactionResult PostVendorManagement(Vendor model, string userid)
        {
            TransactionResult result = new TransactionResult();
            try
            {

                SqlParameter[] parameters = new SqlParameter[] { 
                    new SqlParameter("@VendorNm",model.VendorNm),
                    new SqlParameter("@VendorLocation",model.VendorLocation),
                    new SqlParameter("@ContactPerson",model.ContactPerson),
                    new SqlParameter("@ContactNbr",model.ContactNbr), 
                    new SqlParameter("@IsActive",model.IsActive),
                    new SqlParameter("@userid",userid)
                };

                DataAccessHelper db = new DataAccessHelper();
                result = db.GetData<TransactionResult>("[dbo].[sp_PostVendorManagement]", parameters, CommandType.StoredProcedure)[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public TransactionResult PostPromoManagement(Promo model,List<PromoDetails> modellist, string userid)
        {
            TransactionResult result = new TransactionResult();
            try
            {
                DataTable dtDetails = BuildPromoDetailsTable(modellist);

                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@PromoDesc",model.PromoDesc),
                    new SqlParameter("@DateFrom",model.DateFrom),
                    new SqlParameter("@DateTo",model.DateTo),
                    new SqlParameter("@Price",model.Price),
                    new SqlParameter("@IsActive",model.IsActive),
                    new SqlParameter("@userid",userid),
                    new SqlParameter("@Type",model.Type),
                    new SqlParameter("@PromoDetails", dtDetails)

                };

                DataAccessHelper db = new DataAccessHelper();
                result = db.GetData<TransactionResult>("[dbo].[sp_PostPromoManagement]", parameters, CommandType.StoredProcedure)[0];
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        private DataTable BuildPromoDetailsTable(List<PromoDetails> list)
        {
            if (list == null)
            {
                list = new List<PromoDetails>();
            }

            DataTable dtResult = new DataTable();
            DataRow dr;

            dtResult.Columns.Add("SKU");
            dtResult.Columns.Add("VariantNo");
            dtResult.Columns.Add("BatchNbr");
            dtResult.Columns.Add("Qty");
            dtResult.Columns.Add("Price");
             
            foreach (var item in list)
            {
                dr = dtResult.NewRow(); 
                dr["SKU"] = item.SKU;
                dr["VariantNo"] = item.VariantNo;
                dr["BatchNbr"] = item.BatchNbr;
                dr["Qty"] = item.Qty;
                dr["Price"] = item.Price.Replace(",", ""); 

                dtResult.Rows.Add(dr);
            }

            return dtResult;
        }
        public DataSet GetVendorList(string userId)
        {
            DataSet result = new DataSet();
            try
            {
                string query = "[dbo].[sp_listVendorManagement]";

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
        public DataSet GetPromoList(string userId)
        {
            DataSet result = new DataSet();
            try
            {
                string query = "[dbo].[sp_listPromoManagement]";

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
        public DataSet GetInventoryList(string userId)
        {
            DataSet result = new DataSet();
            try
            {
                string query = "[dbo].[sp_listInventoryManagement]";

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

        public DataSet GetInitializeProductMaintenance(string userId)
        {
            DataSet result = new DataSet();
            try
            {
                string query = "[dbo].[GetProductMaintenanceList]";

                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@param1",userId)
                };

                DataAccessHelper db = new DataAccessHelper();
                result = db.GetData(query, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        
        public DataSet GetBatchList(string userId)
        {
            DataSet result = new DataSet();
            try
            {
                string query = "[dbo].[sp_listBatchManagement]";

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
        public DataSet GetSessionList(string userId)
        {
            DataSet result = new DataSet();
            try
            {
                string query = "[dbo].[sp_listSession]";

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
        public List<CodeDecode> GetSKUMasterList()
        {
            List<CodeDecode> resList = new List<CodeDecode>();
            try
            {

                string query = "[dbo].[GetSKUMasterList]";
                SqlParameter[] parameters = new SqlParameter[] { 
                };

                DataAccessHelper db = new DataAccessHelper();
                resList = db.GetData<CodeDecode>(query, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }

            return resList;
        }
        public List<CodeDecode> FilterInventorySKU()
        {
            List<CodeDecode> resList = new List<CodeDecode>();
            try
            {

                string query = "[dbo].[FilterInventorySKU]";
                SqlParameter[] parameters = new SqlParameter[] {
                };

                DataAccessHelper db = new DataAccessHelper();
                resList = db.GetData<CodeDecode>(query, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }

            return resList;
        }
         

        public DataSet GetChatDetails(string Sender,string Receiver)
        {
            DataSet result = new DataSet();
            try
            {
                string query = "[dbo].[GetChatDetails]";

                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@Sender", Sender),
                    new SqlParameter("@Receiver", Receiver)
                };

                DataAccessHelper db = new DataAccessHelper();
                result = db.GetData(query, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { 
                var Message = ex.Message;
            }

            return result;
        }

        public List<CodeDecode> FilterVariantNo(string param1)
        {
            List<CodeDecode> resList = new List<CodeDecode>();
            try
            {
                string query = "[dbo].[FilterVariantNo]";
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@param1", param1)
                };

                DataAccessHelper db = new DataAccessHelper();
                resList = db.GetData<CodeDecode>(query, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }

            return resList;
        }

        public List<CodeDecode> FilterInventoryVariantNo(string param1)
        {
            List<CodeDecode> resList = new List<CodeDecode>();
            try
            {
                string query = "[dbo].[FilterInventoryVariantNo]";
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@param1", param1)
                };

                DataAccessHelper db = new DataAccessHelper();
                resList = db.GetData<CodeDecode>(query, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }

            return resList;
        }

        public List<CodeDecode> FilterProductPrice(string param1, string param2, string param3)
        {
            List<CodeDecode> resList = new List<CodeDecode>();
            try
            {
                string query = "[dbo].[FilterProductPrice]";
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@param1", param1),
                    new SqlParameter("@param2", param2),
                    new SqlParameter("@param3", param3)
                };

                DataAccessHelper db = new DataAccessHelper();
                resList = db.GetData<CodeDecode>(query, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }

            return resList;
        }

        public List<CodeDecode> GetInventoryOnHand(string param1, string param2, string param3)
        {
            List<CodeDecode> resList = new List<CodeDecode>();
            try
            {
                string query = "[dbo].[GetInventoryOnHand]";
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@param1", param1),
                    new SqlParameter("@param2", param2),
                    new SqlParameter("@param3", param3)
                };

                DataAccessHelper db = new DataAccessHelper();
                resList = db.GetData<CodeDecode>(query, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }

            return resList;
        }

        public List<Vendor> FilterVendorDetails(string param1)
        {
            List<Vendor> resList = new List<Vendor>();
            try
            {
                string query = "[dbo].[FilterVendorDetails]";
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@param1", param1), 
                };

                DataAccessHelper db = new DataAccessHelper();
                resList = db.GetData<Vendor>(query, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }

            return resList;
        }
        
        public List<CodeDecode> FilterInventoryBatchNbr(string param1,string param2)
        {
            List<CodeDecode> resList = new List<CodeDecode>();
            try
            {
                string query = "[dbo].[FilterInventoryBatchNbr]";
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@param1", param1),
                    new SqlParameter("@param2", param2)
                };

                DataAccessHelper db = new DataAccessHelper();
                resList = db.GetData<CodeDecode>(query, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }

            return resList;
        }
        public List<CodeDecode> FilterBatchNbr(string param1,string param2)
        {
            List<CodeDecode> resList = new List<CodeDecode>();
            try
            {
                string query = "[dbo].[FilterBatchNbr]";
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@param1", param1),
                    new SqlParameter("@param2", param2)
                };

                DataAccessHelper db = new DataAccessHelper();
                resList = db.GetData<CodeDecode>(query, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }

            return resList;
        }
        public List<CodeDecode> GetBatchMasterList()
        {
            List<CodeDecode> resList = new List<CodeDecode>();
            try
            {

                string query = "[dbo].[GetBatchMasterList]";
                SqlParameter[] parameters = new SqlParameter[] {
                };

                DataAccessHelper db = new DataAccessHelper();
                resList = db.GetData<CodeDecode>(query, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }

            return resList;
        }
        public List<CodeDecode> GetVariantNoMasterList()
        {
            List<CodeDecode> resList = new List<CodeDecode>();
            try
            {

                string query = "[dbo].[GetVariantNoMasterList]";
                SqlParameter[] parameters = new SqlParameter[] {
                };

                DataAccessHelper db = new DataAccessHelper();
                resList = db.GetData<CodeDecode>(query, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

            }

            return resList;
        }
         
    }
}