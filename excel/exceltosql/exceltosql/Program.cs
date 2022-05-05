using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel_to_data
{
    class Program
    {
        static void Main(string[] args)
        {

            // Connection String to Excel Workbook,Replace DataSource value to point to your excel file location


            string excelconnectionstring = @"provider=microsoft.jet.oledb.4.0;data source=c:\Users\pcuser\Desktop\forrunner.xls
                ; extended properties=" + "\"excel 8.0;hdr=yes;\"";

            // Create Connection to Excel Workbook
            using (OleDbConnection connection =
                         new OleDbConnection(excelconnectionstring))
            {
                OleDbCommand command = new OleDbCommand
                        ("Select * FROM [Sheet1$]", connection);

                connection.Open();

                // Create DbDataReader to Data Worksheet
                using (DbDataReader dr = command.ExecuteReader())
                {
                    // SQL Server Connection String
                    string sqlConnectionString = "Data Source=.;Initial Catalog=db_TheToneHouse;User id=sa;Password=newuser123#;";

                    // Bulk Copy to SQL Server
                    using (SqlBulkCopy bulkCopy =
                               new SqlBulkCopy(sqlConnectionString))
                    {


                        bulkCopy.DestinationTableName = "tb_Diet";
                        bulkCopy.WriteToServer(dr);
                        Console.WriteLine("Data Exoprted To Sql Server Succefully");

                    }
                }
            }
        }
    }
}




