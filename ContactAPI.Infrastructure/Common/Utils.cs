using ContactAPI.Infrastructure.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ContactAPI.Infrastructure.Common
{
    public class Utils
    {
        private static IConfiguration configuration;
        public Utils(IConfiguration iConfig)
        {
            configuration = iConfig;
        }
        public static DataSet ExecuteDataset(string commandText, SqlParameter[] parameters)
        {
            DataSet resultSet = new DataSet();
            string sConStr = configuration.GetSection("MySettings").GetSection("DbConnection").Value; 
            using (var conn = new SqlConnection(sConStr))
            {
                conn.Open();
                var command = conn.CreateCommand() as SqlCommand;
                command.CommandText = commandText;
                command.CommandType = CommandType.StoredProcedure;
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(resultSet);
            }
            return resultSet;
        }

        public static int ExecuteNonQuery(string commandText, SqlParameter[] parameters)
        {
            string sConStr = configuration.GetSection("MySettings").GetSection("DbConnection").Value;
            var conn = new SqlConnection(sConStr);
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = CommandType.StoredProcedure;
            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            var result = command.ExecuteNonQuery();
            conn.Close();
            return result;
        }

        public static DataTable GetContactTable(List<Contact> contacts)
        {
            var Datatable = GetContactTableSchema();
            foreach (var rows in contacts)
            {
                var row = Datatable.NewRow();
                row["firstname"] = rows.firstname;
                row["lastname"] = rows.lastname;
                row["email"] = rows.email;
                row["activeStatus"] = rows.activeStatus;
                row["Id"] = rows.contactId;
                Datatable.Rows.Add(row);
            }
            return Datatable;        
        }

        private static DataTable GetContactTableSchema()        
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", Type.GetType("System.Int32"));
            dt.Columns.Add("firstname", Type.GetType("System.string"));
            dt.Columns.Add("lastname", Type.GetType("System.string"));
            dt.Columns.Add("email", Type.GetType("System.string"));
            dt.Columns.Add("activeStatus", Type.GetType("System.Boolean"));
            return dt;
        }
    }
}
