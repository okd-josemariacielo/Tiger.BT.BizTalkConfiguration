using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace Tiger.BT.BiztalkConfiguration
{
    public class BizTalkAppConfigRetrieve
    {
        /// <summary>
        /// Fetches a config value by key via stored procedure dbo.usp_Config_GetValue.
        /// Returns null if no record exists.
        /// </summary>
        public static string GetConfigValue(string connectionString, string configKey)
        {
            const string storedProcName = "dbo.usp_Config_GetValue";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(storedProcName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Parameter for @configkey
                cmd.Parameters.Add("@configkey", SqlDbType.NVarChar, 100).Value = configKey;

                conn.Open();

                object result = cmd.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                    return null;

                return Convert.ToString(result);
            }
        }
        public static string BizTalkConfigReader(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }  
}

