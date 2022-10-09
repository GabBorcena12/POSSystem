using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace POSSystem
{
    public class DataAccessHelper
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        private SqlTransaction sqlTransaction;

        private string CONN_STRING = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.ToString();

        public DataAccessHelper()
        {
            sqlConnection = new SqlConnection(CONN_STRING);
        }

        public void Open()
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }

            sqlConnection.Open();
        }

        public void Close()
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public string ExecuteScalar(string spName, CommandType commandType)
        {
            return this.ExecuteScalar(spName, new string[] { }, new string[] { }, commandType);
        }

        public string ExecuteScalar(string spName, string[] paramName, string[] paramValues, CommandType commandType)
        {
            Open();

            sqlCommand = new SqlCommand(spName);
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = commandType;

            for (int i = 0; i < paramName.Length; i++)
            {
                sqlCommand.Parameters.Add(new SqlParameter(paramName[i], SqlDbType.VarChar));
                sqlCommand.Parameters[i].Value = paramValues[i];
            }

            string ret = sqlCommand.ExecuteScalar() == null ? string.Empty : sqlCommand.ExecuteScalar().ToString();

            Close();

            return ret;
        }


        public List<T> GetData<T>(string spName, CommandType commandType)
        {
            return this.GetData<T>(spName, new string[] { }, new string[] { }, commandType);
        }

        public List<T> GetData<T>(string spName, string[] paramName, string[] paramValues, CommandType commandType)
        {
            List<T> retList = new List<T>();

            Open();
            sqlCommand = new SqlCommand(spName);
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = commandType;

            for (int i = 0; i < paramName.Length; i++)
            {
                sqlCommand.Parameters.Add(new SqlParameter(paramName[i], paramValues[i]));
            }

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    var item = Activator.CreateInstance<T>();

                    foreach (var property in typeof(T).GetProperties())
                    {
                        try
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                            {
                                Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                                property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
                            }
                        }
                        catch
                        {

                        }

                    }

                    retList.Add(item);
                }
            }

            Close();

            return retList;
        }

        public List<T> GetData<T>(string spName, SqlParameter[] parameters, CommandType commandType)
        {
            List<T> retList = new List<T>();

            Open();
            sqlCommand = new SqlCommand(spName);
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = commandType;
            sqlCommand.Parameters.AddRange(parameters);

            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    var item = Activator.CreateInstance<T>();

                    foreach (var property in typeof(T).GetProperties())
                    {
                        try
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                            {
                                Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                                property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
                            }
                        }
                        catch
                        {

                        }

                    }

                    retList.Add(item);
                }
            }

            Close();

            return retList;
        }

        public DataSet GetData(string spName, SqlParameter[] parameters, CommandType commandType)
        {
            DataSet ds = new DataSet();

            Open();

            sqlCommand = new SqlCommand(spName, sqlConnection);
            sqlCommand.CommandType = commandType;
            sqlCommand.Parameters.AddRange(parameters);

            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            da.Fill(ds);

            Close();

            return ds;
        }

        public int ExecuteNonQuery(string spName, string[] paramName, string[] paramValues, CommandType commandType)
        {
            Open();

            sqlCommand = new SqlCommand(spName);
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = commandType;

            for (int i = 0; i < paramName.Length; i++)
            {
                sqlCommand.Parameters.Add(new SqlParameter(paramName[i], paramValues[i]));
            }

            int ret = sqlCommand.ExecuteNonQuery();

            Close();
            return ret;
        }

        public int UploadImage(string spName, string[] paramName, string id, byte[] image, CommandType commandType)
        {
            Open();

            sqlCommand = new SqlCommand(spName);
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = commandType;

            SqlParameter param0 = new SqlParameter(paramName[0], SqlDbType.VarChar);
            param0.Value = id;
            sqlCommand.Parameters.Add(param0);

            SqlParameter param1 = new SqlParameter(paramName[1], SqlDbType.Image);
            param1.Value = image;
            sqlCommand.Parameters.Add(param1);

            int ret = sqlCommand.ExecuteNonQuery();

            Close();
            return ret;
        }

        public void BeginTransaction()
        {
            sqlTransaction = sqlConnection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (HasActiveTransaction)
            {
                sqlTransaction.Commit();
            }
        }

        public void RollbackTransaction()
        {
            if (HasActiveTransaction)
            {
                sqlTransaction.Rollback();
            }
        }

        public bool HasActiveTransaction
        {
            get { return sqlTransaction != null; }
        }
    }
}