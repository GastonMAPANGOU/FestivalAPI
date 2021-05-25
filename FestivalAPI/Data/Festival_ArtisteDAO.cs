using FestivalAPI.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Data
{
    public class Festival_ArtisteDAO
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FestivalAPIContext-adf8fbfd-4c0b-47bf-a2e7-41bddfef421b;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<int>List_Id_Scene(List<Artiste>Artistes)
        {
            List<int> list_Id_Scene = new List<int>();
            foreach (Artiste artiste in Artistes)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlQuery = "Select IdS from dbo.Festival_Artiste where ArtisteId = " + artiste.IdA +" Group by SceneId";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            list_Id_Scene.Add(reader.GetInt32(0));
                        }
                    }
                }
            }

            return list_Id_Scene;
        }

        public List<Festival_Artiste> List_Festival_Artiste(List<Scene>Scenes)
        {
            List<Festival_Artiste> list_Scene = new List<Festival_Artiste>();
            foreach (Scene scene in Scenes)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlQuery = "Select * from dbo.Festival_Artiste where SceneId = " + scene.IdS;
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Festival_Artiste festival_Artiste = new Festival_Artiste();

                            festival_Artiste.Id = reader.GetInt32(0);
                            festival_Artiste.FestivalId = reader.GetInt32(1);
                            festival_Artiste.ArtisteId = reader.GetInt32(2);
                            festival_Artiste.SceneId = reader.GetInt32(3);
                            festival_Artiste.JourId = reader.GetInt32(4);
                            festival_Artiste.Heure = reader.GetDateTime(5);

                            list_Scene.Add(festival_Artiste);

                            
                        }
                    }
                }
            }

            return list_Scene;
        }

        public List<Festival_Artiste> List_Festival_Artiste_Jour(int JourId)
        {
            List<Festival_Artiste> list_Scene = new List<Festival_Artiste>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlQuery = "Select * from dbo.Festival_Artiste where JourId = " + JourId;
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Festival_Artiste festival_Artiste = new Festival_Artiste();

                            festival_Artiste.Id = reader.GetInt32(0);
                            festival_Artiste.FestivalId = reader.GetInt32(1);
                            festival_Artiste.ArtisteId = reader.GetInt32(2);
                            festival_Artiste.SceneId = reader.GetInt32(3);
                            festival_Artiste.JourId = reader.GetInt32(4);
                            festival_Artiste.Heure = reader.GetDateTime(5);

                            list_Scene.Add(festival_Artiste);


                        }
                    }
                }

            return list_Scene;
        }

        public List<Festival_Artiste> List_Festival_Artiste_Heure(DateTime Heure)
        {
            List<Festival_Artiste> list_Scene = new List<Festival_Artiste>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select * from dbo.Festival_Artiste where Heure = " + Heure;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Festival_Artiste festival_Artiste = new Festival_Artiste();

                        festival_Artiste.Id = reader.GetInt32(0);
                        festival_Artiste.FestivalId = reader.GetInt32(1);
                        festival_Artiste.ArtisteId = reader.GetInt32(2);
                        festival_Artiste.SceneId = reader.GetInt32(3);
                        festival_Artiste.JourId = reader.GetInt32(4);
                        festival_Artiste.Heure = reader.GetDateTime(5);

                        list_Scene.Add(festival_Artiste);


                    }
                }
            }

            return list_Scene;
        }

        public List<Festival_Artiste> List_Festival_Artiste_Artiste(int ArtsiteId)
        {
            List<Festival_Artiste> list_Scene = new List<Festival_Artiste>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select * from dbo.Festival_Artiste where ArtisteId = " + ArtsiteId;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Festival_Artiste festival_Artiste = new Festival_Artiste();

                        festival_Artiste.Id = reader.GetInt32(0);
                        festival_Artiste.FestivalId = reader.GetInt32(1);
                        festival_Artiste.ArtisteId = reader.GetInt32(2);
                        festival_Artiste.SceneId = reader.GetInt32(3);
                        festival_Artiste.JourId = reader.GetInt32(4);
                        festival_Artiste.Heure = reader.GetDateTime(5);

                        list_Scene.Add(festival_Artiste);


                    }
                }
            }

            return list_Scene;
        }

        public List<Festival_Artiste>list_Festival_Artiste(int IdS, int IdA)
        {
            List<Festival_Artiste> list = new List<Festival_Artiste>();
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select Festival_Artiste.Id, Festival_Artiste.ArtisteId, Festival_Artiste.FestivalId" +
                    "Festival_Artiste.JourId, Festival_Artiste.SceneId from dbo.Festival_Artiste " +
                    "inner join Artiste on Festival_Artiste.ArtisteId = Artiste.IdA " +
                    "inner join Style.Id = Artiste.StyleId where Artiste.IdA = " + IdA +
                    " and Style.Id = " + IdS;

                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        Festival_Artiste festival_Artiste = new Festival_Artiste();

                        festival_Artiste.Id = reader.GetInt32(0);
                        festival_Artiste.ArtisteId = reader.GetInt32(2);
                        festival_Artiste.FestivalId = reader.GetInt32(1);
                        festival_Artiste.JourId = reader.GetInt32(4);
                        festival_Artiste.SceneId = reader.GetInt32(3);

                        list.Add(festival_Artiste);
                    }
                }
            }

            return list;
        }
    }
}
