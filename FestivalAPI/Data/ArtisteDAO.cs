using FestivalAPI.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Data
{
    public class ArtisteDAO
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FestivalAPIContext-adf8fbfd-4c0b-47bf-a2e7-41bddfef421b;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<Artiste> List_Artiste()
        {
            List<Artiste> list_Artiste = new List<Artiste>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select * from Artiste";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Artiste artiste = new Artiste();

                        artiste.IdA = reader.GetInt32(0);
                        artiste.Nom = reader.GetString(1);
                        artiste.PaysId = reader.GetInt32(2);
                        artiste.Photo = reader.GetString(3);
                        artiste.Prenom = reader.GetString(4);
                        artiste.StyleId = reader.GetInt32(5);
                        

                        list_Artiste.Add(artiste);
                    }
                }

                return list_Artiste;
            }
        }

        public List<String> List_Artiste_Nom()
        {
            List<String> list_Artiste = new List<String>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select Nom from Artiste";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {   
                        list_Artiste.Add(reader.GetString(0));
                    }
                }

                return list_Artiste;
            }
        }

        public int Return_IdArtiste(string search)
        {
            int Id = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select IdA from Artist where nom = " + search;
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

        public List<Artiste>Return_Artiste_By_Style(int StyleId)
        {
            List<Artiste> artistes = new List<Artiste>();

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select * from Artiste where StyleId = " + StyleId;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        Artiste artiste = new Artiste();
                        
                        artiste.IdA = reader.GetInt32(0);
                        artiste.Nom = reader.GetString(1);
                        artiste.Prenom = reader.GetString(2);
                        artiste.Photo = reader.GetString(3);
                        artiste.StyleId = reader.GetInt32(4);
                        artiste.Descriptif = reader.GetString(5);
                        artiste.PaysId = reader.GetInt32(6);
                        artiste.Extrait = reader.GetString(7);
                        
                        artistes.Add(artiste);
                    }
                }

            }


            return (artistes);
        }
    }
}
