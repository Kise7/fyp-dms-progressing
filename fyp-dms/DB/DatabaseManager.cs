using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections;


namespace fyp_dms.DB
{
    public class DatabaseManager
    {
        private string strConn;
        private MySqlConnection sqlConn;
        private MySqlCommand sqlCmd;

        public DatabaseManager()
        {

        }

        private void OpenConnection()
        {
            try
            {
                strConn = "server = localhost; user = root; database = document; port = 3306; password =;CharSet=ascii";
                sqlConn = new MySqlConnection(strConn);
                sqlCmd = new MySqlCommand();
                sqlCmd.Connection = sqlConn;

                sqlConn.Open();
            }
            catch (Exception exception)
            {
                throw exception;
            }

        }

        public void CloseConnection()
        {
            try
            {
                sqlConn.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void ExecuteNonQuery(object query)
        {
            try
            {
                OpenConnection();

                sqlCmd = (MySqlCommand)query;
                sqlCmd.Connection = sqlConn;
                sqlCmd.ExecuteNonQuery();

                CloseConnection();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public object GetDataScalar(object query)
        {
            object scalar = new object();

            try
            {
                OpenConnection();

                sqlCmd = (MySqlCommand)query;
                sqlCmd.Connection = sqlConn;
                scalar = sqlCmd.ExecuteScalar();

                CloseConnection();
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return scalar;
        }

        public int GetIntScalar(object query)
        {
            int scalar;

            try
            {
                OpenConnection();

                sqlCmd = (MySqlCommand)query;
                sqlCmd.Connection = sqlConn;
                scalar = Convert.ToInt32(sqlCmd.ExecuteScalar());

                CloseConnection();
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return scalar;
        }

        public string GetStringScalar(object query)
        {
            string scalar;

            try
            {
                OpenConnection();


                sqlCmd = (MySqlCommand)query;
                sqlCmd.Connection = sqlConn;
                scalar = sqlCmd.ExecuteScalar().ToString();

                CloseConnection();
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return scalar;
        }


        public object GetDataReader(object query)
        {
            object reader = new object();

            try
            {
                OpenConnection();

                sqlCmd = (MySqlCommand)query;
                sqlCmd.Connection = sqlConn;
                reader = sqlCmd.ExecuteReader();

            }
            catch (Exception exception)
            {
                throw exception;
            }

            return reader;
        }

        public DataSet GetDataSet(object query, string tablename)
        {
            DataSet ds = new DataSet();

            try
            {
                OpenConnection();
                sqlCmd = (MySqlCommand)query;
                sqlCmd.Connection = sqlConn;
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCmd);
                adapter.Fill(ds, tablename);
                CloseConnection();
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return ds;
        }


        public ArrayList GetArrayList(object query)
        {
            ArrayList drContainer = null;
            int iFieldCount = 0;

            try
            {
                OpenConnection();

                sqlCmd = (MySqlCommand)query;
                sqlCmd.Connection = sqlConn;
                MySqlDataReader reader = (MySqlDataReader)sqlCmd.ExecuteReader();

                drContainer = new ArrayList();
                while (reader.Read())
                {
                    iFieldCount = reader.FieldCount;//get count of the number of columns selected
                    for (int i = 0; i < iFieldCount; i++)
                    {
                        //check is the result return is NULL. If it's NULL and datatype is string, will return "", 
                        //else if datatype is int, will return 0
                        //add the valid data into arraylist 
                        drContainer.Add(reader.IsDBNull(i) ? ResetEmptyObject(reader.GetFieldType(i)) : reader.GetValue(i));
                    }
                }
                if (reader != null) ((IDisposable)reader).Dispose();//Releasing the SqlDataReader object

                CloseConnection();
            }

            catch (Exception exception)
            {
                throw exception;
            }
            return drContainer;
        }

        public ArrayList GetConcatCode(ArrayList alTempResult, string strID, string strValue)
        {
            DictionaryEntry de = new DictionaryEntry();
            ArrayList arResult = new ArrayList();

            if (alTempResult.Count > 0)
            {
                de.Key = "";
                de.Value = "Please Choose";
                arResult.Add(de);

                if (strID != "")
                {
                    de.Key = strID;
                    de.Value = strValue;
                    arResult.Add(de);
                }

                for (int i = 0; i < alTempResult.Count; i += 2)
                {
                    de.Key = alTempResult[i];
                    de.Value = alTempResult[i + 1];
                    arResult.Add(de);
                }
            }

            return arResult;
        }

        /// <summary>
        /// Reset object into empty form.
        /// </summary>
        /// <param name="readerType">a specified data type</param>
        /// <remarks>
        /// Pseudocode
        /// <para>- If readerType is string then return ""</para>
        /// <para>- else if readerType is int then return 0.</para>
        /// <para>- else return null.</para>		
        /// </remarks>
        private object ResetEmptyObject(System.Type readerType)
        {
            if (readerType == typeof(System.String))
                return "";
            else if (readerType == typeof(int))
                return 0;
            else return null;
        }
    }
}