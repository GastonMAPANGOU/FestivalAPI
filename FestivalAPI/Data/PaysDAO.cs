using FestivalAPI.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Data
{
    public class PaysDAO
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FestivalAPIContext-adf8fbfd-4c0b-47bf-a2e7-41bddfef421b;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<Pays> List_Pays()
        {
            List<Pays> list_Pays = new List<Pays>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select * from Pays";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Pays pays = new Pays();

                        pays.Id = reader.GetInt32(0);
                        pays.Nom = reader.GetString(1);


                        list_Pays.Add(pays);
                    }
                }

                return list_Pays;
            }
        }

        public List<String> List_Pays_Nom()
        {
            List<String> list_Pays = new List<String>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select Nom from Pays";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list_Pays.Add(reader.GetString(0));
                    }
                }

                return list_Pays;
            }
        }
    }
}
