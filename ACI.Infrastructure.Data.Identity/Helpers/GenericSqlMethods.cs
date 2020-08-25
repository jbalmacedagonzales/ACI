using ACI.Infrastructure.CrossCutting.Logging;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ACI.Infrastructure.Data.Identity.Helpers
{
    public static class GenericSqlMethods
    {

        public static async Task<int> ExecuteNonQueryAsync(string connectionString, string cmdText, SqlParameter[] parameters)
        {
            int result = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(cmdText, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (var item in parameters)
                    {
                        command.Parameters.AddWithValue(item.ParameterName, item.Value);
                    }
                    result = await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(ex.Message, ex.StackTrace);
                }
            }
            return result;
        }

        public static async Task<SqlDataReader> ExecuteReaderAsync(string connectionString, string cmdText, SqlParameter[] parameters)
        {
            SqlDataReader reader = null;
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand(cmdText, connection);
                command.CommandType = CommandType.StoredProcedure;
                foreach (var item in parameters)
                {
                    command.Parameters.AddWithValue(item.ParameterName, item.Value);
                }
                reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message, ex.StackTrace);
            }
            return reader;
        }


    }
}
