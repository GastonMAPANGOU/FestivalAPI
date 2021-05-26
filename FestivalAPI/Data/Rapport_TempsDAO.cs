using FestivalAPI.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Data
{
    public class Rapport_TempsDAO
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FestivalAPIContext-adf8fbfd-4c0b-47bf-a2e7-41bddfef421b;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        
        public List<Rapport_Temps>Rapport_Temps_Jour(int FestivalId, DateTime date_Inscription)
        {
            List<Rapport_Temps> rapport_Temps = new List<Rapport_Temps>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select * from Rapport_Temps where FestivalId = " + FestivalId + "AND Date_Inscription = " + date_Inscription;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Rapport_Temps rapport = new Rapport_Temps();

                        rapport.Id = reader.GetInt32(0);
                        rapport.Date_Inscription = reader.GetDateTime(1);
                        rapport.FestivalId = reader.GetInt32(2);
                        rapport.Nombre_Inscription = reader.GetInt32(3);

                        rapport_Temps.Add(rapport);

                    }
                }
            }
            return rapport_Temps;
        }
        public List<Rapport_Temps> Rapport_Temps_Festival(int FestivalId)
        {
            List<Rapport_Temps> rapport_Temps = new List<Rapport_Temps>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select * from Rapport_Temps where FestivalId = " + FestivalId;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Rapport_Temps rapport = new Rapport_Temps();

                        rapport.Id = reader.GetInt32(0);
                        rapport.Date_Inscription = reader.GetDateTime(1);
                        rapport.FestivalId = reader.GetInt32(2);
                        rapport.Nombre_Inscription = reader.GetInt32(3);

                        rapport_Temps.Add(rapport);

                    }
                }
            }
            return rapport_Temps;
        }


        internal Rapport_Temps Rapport_Temps_Jour(int? id, DateTime? dateInscription)
        {
            throw new NotImplementedException();
        }
    }
}
