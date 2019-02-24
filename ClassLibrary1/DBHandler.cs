using System;
using System.Data.SqlClient;

namespace DbHandler
{
    public class DbHandler
    {

        //TODO: Throw exceptions for ExecuteReader and ExecuteNonQuery and catch on a higher level
        //TODO: Implement ExecuteScalar?
        /// <summary>
        /// Used for select operations or stored procedures that return one or several values
        /// </summary>
        /// <param name="connection">The database connection object</param>
        /// <param name="command">The SQL command to be executed</param>
        /// <param name="useStoredProcedure">Whether or not the command should be executed as a stored procedure</param>
        /// <returns>An SqlDataReader object containing the zero-multiple lines returned by the execution</returns>
        /// 
        public static SqlDataReader ExecuteReader(string connectionString, string queryText, bool useStoredProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Use temporary connection to keep datareader from closing, see https://social.msdn.microsoft.com/Forums/en-US/bc2ef0d3-597a-433e-9cfe-c6726ae5e2e9/return-type-sqldatareader-method-problem?forum=adodotnetdataproviders
                SqlConnection tempConn = new SqlConnection(connection.ConnectionString);
                try
                {
                    tempConn.Open();
                    using (SqlCommand command = new SqlCommand(queryText, tempConn))
                    {
                        if (useStoredProcedure)
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                        }

                        var dataReader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                        return dataReader;
                    }
                }

                catch (Exception e)
                {
                    tempConn.Close();
                    return null;
                }
            }
        }

        /// <summary>
        /// Used for insert/update/delete operations or stored procedures that don't return a value
        /// </summary>
        /// <param name="connection">The database connection object</param>
        /// <param name="command">The SQL command to be executed</param>
        /// <param name="useStoredProcedure">Whether or not the command should be executed as a stored procedure</param>
        /// <returns>An int containing the number of rows affected</returns>

        public static int ExecuteNonQuery(string connectionString, string queryText, bool useStoredProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var query = queryText;
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        var result = command.ExecuteNonQuery();
                        return result;
                    }
                }
                catch (Exception e)
                {
                    return -1;
                }
            }
        }
    }
}
