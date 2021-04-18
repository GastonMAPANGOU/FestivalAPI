using FestivalAPI.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Data
{
    public class SceneDAO
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FestivalAPIContext-adf8fbfd-4c0b-47bf-a2e7-41bddfef421b;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<Scene> Display_By_Lieu(int LieuId)
        {
            List<Scene> list_Scene = new List<Scene>();

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select * from Scene where LieuId = " + LieuId;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        Scene scene = new Scene();

                        scene.IdS = reader.GetInt32(0);
                        scene.Nom = reader.GetString(1);
                        scene.Adresse = reader.GetString(2);
                        scene.Capacite = reader.GetInt32(3);
                        scene.Accessibilite = reader.GetInt32(4);
                        scene.LieuId = reader.GetInt32(5);

                        list_Scene.Add(scene);
                    }
                }
                return list_Scene;
            }
        }

        public List<Scene>List_Scene(List<Festival_Artiste>list_Festival_Artiste)
        {
            List<Scene> list_Scene = new List<Scene>();
            foreach (Festival_Artiste festival_Artiste in list_Festival_Artiste)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlQuery = "Select * from Scene where IdS = " + festival_Artiste.SceneId;
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Scene scene = new Scene();

                            scene.IdS = reader.GetInt32(0);
                            scene.Nom = reader.GetString(1);
                            scene.Adresse = reader.GetString(2);
                            scene.Capacite = reader.GetInt32(3);
                            scene.Accessibilite = reader.GetInt32(4);
                            scene.LieuId = reader.GetInt32(5);

                            list_Scene.Add(scene);
                        }
                    }
                }
            }

            return list_Scene;
        }

        public List<Scene> List_Scene_by_Artist_Lieu_Style(List<Festival_Artiste> list_Festival_Artiste, int LieuId)
        {
            List<Scene> list_Scene = new List<Scene>();
            foreach (Festival_Artiste festival_Artiste in list_Festival_Artiste)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlQuery = "Select * from Scene where IdS = " + festival_Artiste.SceneId + "and LieuId = "+LieuId;
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Scene scene = new Scene();

                            scene.IdS = reader.GetInt32(0);
                            scene.Nom = reader.GetString(1);
                            scene.Adresse = reader.GetString(2);
                            scene.Capacite = reader.GetInt32(3);
                            scene.Accessibilite = reader.GetInt32(4);
                            scene.LieuId = reader.GetInt32(5);

                            list_Scene.Add(scene);
                        }
                    }
                }
            }

            return list_Scene;
        }
    }
}
