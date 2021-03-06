using FestivalAPI.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Data
{
    public class Rapport_GeoDAO
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FestivalAPIContext-adf8fbfd-4c0b-47bf-a2e7-41bddfef421b;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<Rapport_Geo> Rapport_Geo_Departement(string Departement)
        {
            List<Rapport_Geo> rapport_Geos = new List<Rapport_Geo>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select * from Rapport_Geo where Departement = '" + Departement +"'";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Rapport_Geo rapport_Geo = new Rapport_Geo();

                        rapport_Geo.Id = reader.GetInt32(0);
                        rapport_Geo.FestivalId = reader.GetInt32(1);
                        rapport_Geo.FestivalierId = reader.GetInt32(2);
                        rapport_Geo.Pays = reader.GetString(3);
                        rapport_Geo.Departement = reader.GetString(4);
                        rapport_Geo.Region = reader.GetString(5);
                        rapport_Geo.Genre = reader.GetString(6);

                        rapport_Geos.Add(rapport_Geo);
                    }
                }
            }
            return rapport_Geos;
        }

        public List<Rapport_Geo> Rapport_Geo_Region(string Region)
        {
            List<Rapport_Geo> rapport_Geos = new List<Rapport_Geo>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select * from Rapport_Geo where Region = '" + Region +"'";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Rapport_Geo rapport_Geo = new Rapport_Geo();

                        rapport_Geo.Id = reader.GetInt32(0);
                        rapport_Geo.FestivalId = reader.GetInt32(1);
                        rapport_Geo.FestivalierId = reader.GetInt32(2);
                        rapport_Geo.Pays = reader.GetString(3);
                        rapport_Geo.Departement = reader.GetString(4);
                        rapport_Geo.Region = reader.GetString(5);
                        rapport_Geo.Genre = reader.GetString(6);

                        rapport_Geos.Add(rapport_Geo);
                    }
                }
            }
            return rapport_Geos;
        }

        public List<Rapport_Geo> Rapport_Geo_Pays(string Pays)
        {
            List<Rapport_Geo> rapport_Geos = new List<Rapport_Geo>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select * from Rapport_Geo where Pays = '" + Pays +"'";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Rapport_Geo rapport_Geo = new Rapport_Geo();

                        rapport_Geo.Id = reader.GetInt32(0);
                        rapport_Geo.FestivalId = reader.GetInt32(1);
                        rapport_Geo.FestivalierId = reader.GetInt32(2);
                        rapport_Geo.Pays = reader.GetString(3);
                        rapport_Geo.Departement = reader.GetString(4);
                        rapport_Geo.Region = reader.GetString(5);
                        rapport_Geo.Genre = reader.GetString(6);

                        rapport_Geos.Add(rapport_Geo);
                    }
                }
            }
            return rapport_Geos;
        }

        public List<Rapport_Geo> Rapport_Geo_Genre(string Genre)
        {
            List<Rapport_Geo> rapport_Geos = new List<Rapport_Geo>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select * from Rapport_Geo where Genre = '" + Genre +"'";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Rapport_Geo rapport_Geo = new Rapport_Geo();

                        rapport_Geo.Id = reader.GetInt32(0);
                        rapport_Geo.FestivalId = reader.GetInt32(1);
                        rapport_Geo.FestivalierId = reader.GetInt32(2);
                        rapport_Geo.Pays = reader.GetString(3);
                        rapport_Geo.Departement = reader.GetString(4);
                        rapport_Geo.Region = reader.GetString(5);
                        rapport_Geo.Genre = reader.GetString(6);

                        rapport_Geos.Add(rapport_Geo);
                    }
                }
            }
            return rapport_Geos;
        }

        public List<int> Rapport_Geo_Count_Pays(int FestivalId, string Pays)
        {
            List<int> count = new List<int>();

            List<Rapport_Geo> rapport_Geos = new List<Rapport_Geo>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select count(FestivalierId) from Rapport_Geo where FestivalId = " + FestivalId + "And Pays = '"+ Pays +"' group by Region";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count.Add(reader.GetInt32(0));
                    }
                }
            }
            return count;
        }

        public List<int> Rapport_Geo_Count_Departement(int FestivalId, string Departement)
        {
            List<int> count = new List<int>();

            List<Rapport_Geo> rapport_Geos = new List<Rapport_Geo>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select count(FestivalierId) from Rapport_Geo where FestivalId = " + FestivalId + "And Departement = '" + Departement + "' group by Region";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count.Add(reader.GetInt32(0));
                    }
                }
            }
            return count;
        }

        public List<int> Rapport_Geo_Count_Region(int FestivalId, string Region)
        {
            List<int> count = new List<int>();

            List<Rapport_Geo> rapport_Geos = new List<Rapport_Geo>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select count(FestivalierId) from Rapport_Geo where FestivalId = " + FestivalId + "And Region = '" + Region + "' group by Genre";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count.Add(reader.GetInt32(0));
                    }
                }
            }
            return count;
        }
    }
}
