using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopManagmentSystem.Common
{
    internal class DBConnection
    {
        
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter dataAdaptor;
        private SqlCommandBuilder commandBuilder;
        private String connectionString = @"Data Source=Dell\SQLEXPRESS01;Initial Catalog=ManagmentSystem;Integrated Security=True";

        public DBConnection()
        {
            connection = new SqlConnection(connectionString);
            command = new SqlCommand();
            command.Connection = connection;
        }
        
        public static DBConnection getDBConnection()
        {
            return new DBConnection();
        }

        public int executeInsertUpdateQuery(string Query)
        {
            int count = 0;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            command.CommandText = Query;
            count = command.ExecuteNonQuery();
            connection.Close();
            return count;
        }

        public DataTable getDataTable(string selectCommand)
        {
            dataAdaptor = new SqlDataAdapter(selectCommand, connectionString);
            commandBuilder = new SqlCommandBuilder(dataAdaptor);
            DataTable table = new DataTable
            {
                Locale = CultureInfo.InvariantCulture
            };
            dataAdaptor.Fill(table);
            return table;
        }
    }
}
