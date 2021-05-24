using FestivalAPI.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Data
{
    public class Rapport_ActiviteDAO
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FestivalAPIContext-adf8fbfd-4c0b-47bf-a2e7-41bddfef421b;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<Rapport_Activite>Rapport_Activite_Departement(string Departement)
        {
            List<Rapport_Activite>rapport_Activites = new List<Rapport_Activite>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select * from Rapport_Activite where Departement = '" + Departement +"'";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Rapport_Activite rapport_Activite = new Rapport_Activite();

                        rapport_Activite.Id = reader.GetInt32(0);
                        rapport_Activite.Annee = reader.GetInt32(1);
                        rapport_Activite.FestivalId = reader.GetInt32(2);
                        rapport_Activite.Departement = reader.GetString(3);
                        rapport_Activite.Region = reader.GetString(4);
                        rapport_Activite.Somme_Vente = reader.GetDouble(5);

                        rapport_Activites.Add(rapport_Activite);
                    }
                }
            }
            return rapport_Activites;
        }

        public List<Rapport_Activite> Rapport_Activite_Region(string Region)
        {
            List<Rapport_Activite> rapport_Activites = new List<Rapport_Activite>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select * from Rapport_Activite where Region = '" + Region +"'" ;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Rapport_Activite rapport_Activite = new Rapport_Activite();

                        rapport_Activite.Id = reader.GetInt32(0);
                        rapport_Activite.Annee = reader.GetInt32(1);
                        rapport_Activite.FestivalId = reader.GetInt32(2);
                        rapport_Activite.Departement = reader.GetString(3);
                        rapport_Activite.Region = reader.GetString(4);
                        rapport_Activite.Somme_Vente = reader.GetDouble(5);

                        rapport_Activites.Add(rapport_Activite);
                    }
                }
            }
            return rapport_Activites;
        }

        public List<Rapport_Activite> Rapport_Activite_Festival(int FestivalId)
        {
            List<Rapport_Activite> rapport_Activites = new List<Rapport_Activite>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select * from Rapport_Activite where FestivalId = " + FestivalId;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Rapport_Activite rapport_Activite = new Rapport_Activite();

                        rapport_Activite.Id = reader.GetInt32(0);
                        rapport_Activite.Annee = reader.GetInt32(1);
                        rapport_Activite.FestivalId = reader.GetInt32(2);
                        rapport_Activite.Departement = reader.GetString(3);
                        rapport_Activite.Region = reader.GetString(4);
                        rapport_Activite.Somme_Vente = reader.GetDouble(5);

                        rapport_Activites.Add(rapport_Activite);
                    }
                }
            }
            return rapport_Activites;
        }
    }
}
