using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FestivalAPI.Models;
using Microsoft.Data.SqlClient;

namespace FestivalAPI.Data
{
    public class FestivalierDAO
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FestivalAPIContext-adf8fbfd-4c0b-47bf-a2e7-41bddfef421b;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<Festivalier> list_festivalier()
        {
           List<Festivalier> returnList = new List<Festivalier>();

            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                string sqlQuery = "Select * from Festivalier";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        Festivalier festivalier = new Festivalier();

                        festivalier.Id = reader.GetInt32(0);
                        festivalier.Nom = reader.GetString(1);
                        festivalier.Prenom = reader.GetString(2);
                        festivalier.Login = reader.GetString(3);
                        festivalier.Pwd = reader.GetString(4);
                        festivalier.NbJours = reader.GetInt32(5);
                        festivalier.Nb_ParticipantsPT = reader.GetInt32(6);
                        festivalier.Nb_ParticipantsDT = reader.GetInt32(7);
                        festivalier.Age = reader.GetInt32(8);
                        festivalier.Genre = reader.GetString(9);
                        festivalier.LieuId = reader.GetInt32(10);
                        festivalier.InscriptionAccepted = reader.GetBoolean(11);
                        festivalier.Somme = reader.GetDouble(12);
                        festivalier.FestivalId = reader.GetInt32(13);

                        returnList.Add(festivalier);


                    }
                }
            }

            return returnList;
        }

        public Festivalier Display_One_Festivalier(int Id)
        {
            Festivalier festivalier = new Festivalier();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select * from Festivalier where Id = " + Id;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = Id;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        festivalier.Id = reader.GetInt32(0);
                        festivalier.Nom = reader.GetString(1);
                        festivalier.Prenom = reader.GetString(2);
                        festivalier.Login = reader.GetString(3);
                        festivalier.Pwd = reader.GetString(4);
                        festivalier.NbJours = reader.GetInt32(5);
                        festivalier.Nb_ParticipantsPT = reader.GetInt32(6);
                        festivalier.Nb_ParticipantsDT = reader.GetInt32(7);
                        festivalier.Age = reader.GetInt32(8);
                        festivalier.Genre = reader.GetString(9);
                        festivalier.LieuId = reader.GetInt32(10);
                        festivalier.InscriptionAccepted = reader.GetBoolean(11);
                        festivalier.Somme = reader.GetDouble(12);
                        festivalier.FestivalId = reader.GetInt32(13);

                    }
                }
            }
   
            return festivalier;
        }

        public Festivalier Display_One_Festivalier_Login(string Login, int FestivalId)
        {
            Festivalier festivalier = new Festivalier();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select * from Festivalier where Login = " + Login + "and FestivalId = " +FestivalId;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        festivalier.Id = reader.GetInt32(0);
                        festivalier.Nom = reader.GetString(1);
                        festivalier.Prenom = reader.GetString(2);
                        festivalier.Login = reader.GetString(3);
                        festivalier.Pwd = reader.GetString(4);
                        festivalier.NbJours = reader.GetInt32(5);
                        festivalier.Nb_ParticipantsPT = reader.GetInt32(6);
                        festivalier.Nb_ParticipantsDT = reader.GetInt32(7);
                        festivalier.Age = reader.GetInt32(8);
                        festivalier.Genre = reader.GetString(9);
                        festivalier.LieuId = reader.GetInt32(10);
                        festivalier.InscriptionAccepted = reader.GetBoolean(11);
                        festivalier.Somme = reader.GetDouble(12);
                        festivalier.FestivalId = reader.GetInt32(13);

                    }
                }
            }

            return festivalier;
        }

        public int Delete_Id(int Id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using(SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "Delete from Festivalier where Id = " + Id;
                connection.Open();
                int DeleteId = command.ExecuteNonQuery();
                return DeleteId;
            }
        }

    }
}
