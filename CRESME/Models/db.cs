
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace CRESME.Models
{

    /*This class will create a database object and it has a function called GetRecord that retrives the students grades and records from the database and outputs it as an excel file */
    public class db
    {

        private SqlConnection con;
        private IConfiguration _config;

        public db()
        {
            /*_config = config;*/
           var configuration = GetConfiguration();
            
            con = new SqlConnection(configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        }


        public IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

        /*This fucntion retrives the student grades and data from the database and exports it as an excel file.*/
        public DataSet Getrecord()
        {
            string studentData = "select * from [CRESME].[dbo].[AspNetUsers]"; 
            SqlCommand com = new SqlCommand(studentData, con);
            /*com.CommandType = CommandType.StoredProcedure;*/
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds; 


        }


    }
}
