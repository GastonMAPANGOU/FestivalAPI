using FestivalAPI.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Data
{
    public class LieuDAO
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FestivalAPIContext-adf8fbfd-4c0b-47bf-a2e7-41bddfef421b;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<Lieu>List_Lieux()
        {
            List<Lieu> list_Lieux = new List<Lieu>();

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select * from Lieu";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        Lieu lieu = new Lieu();

                        lieu.IdL = reader.GetInt32(0);
                        lieu.Commune = reader.GetString(1);

                        list_Lieux.Add(lieu);
                    }
                }

                return list_Lieux;
            }
        }

        public List<String> List_Lieu_Nom()
        {
            List<String> list_Lieu = new List<String>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select Nom from Lieu";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list_Lieu.Add(reader.GetString(0));
                    }
                }

                return list_Lieu;
            }
        }

        public int Return_IdLieu(string search)
        {
            int Id = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select Id from Lieu where nom = " + search;
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
