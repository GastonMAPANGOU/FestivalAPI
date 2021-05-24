using FestivalAPI.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Data
{
    public class StyleDAO
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FestivalAPIContext-adf8fbfd-4c0b-47bf-a2e7-41bddfef421b;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<Style> List_Style()
        {
            List<Style> list_Style = new List<Style>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select * from dbo.Style";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Style style = new Style();

                        style.Id = reader.GetInt32(0);
                        style.Nom = reader.GetString(1);


                        list_Style.Add(style);
                    }
                }

                return list_Style;
            }
        }

        public List<String> List_Style_Nom()
        {
            List<String> list_Style = new List<String>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select Nom from dbo.Style";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list_Style.Add(reader.GetString(0));
                    }
                }

                return list_Style;
            }
        }

        public int Return_IdStyle(string search)
        {
            int Id = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select Id from dbo.Style where nom = "+search;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                       Id = reader.GetInt32(0);
                    }
                }
                return Id;
            }
        }
    }
}
