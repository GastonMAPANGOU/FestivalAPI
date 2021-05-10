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

        public Scene GetScene_Nom(string Nom)
        {
            Scene scene = new Scene();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select * from Scene where Nom = " + Nom;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        scene.IdS = reader.GetInt32(0);
                        scene.Nom = reader.GetString(1);
                        scene.Adresse = reader.GetString(2);
                        scene.Capacite = reader.GetInt32(3);
                        scene.Accessibilite = reader.GetString(2);
                        scene.LieuId = reader.GetInt32(5);

                       
                    }
                }

                return scene;
            }
        }

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
                        scene.Accessibilite = reader.GetString(2);
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
                            scene.Accessibilite = reader.GetString(2);
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
                            scene.Accessibilite = reader.GetString(2);
                            scene.LieuId = reader.GetInt32(5);

                            list_Scene.Add(scene);
                        }
                    }
                }
            }

            return list_Scene;
        }

        public List<Scene> List_Scene_Id(List<int> list_Id_Scene)
        {

            List<Scene> list_Scene = new List<Scene>();

            foreach (int i in list_Id_Scene)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlQuery = "Select * from Scene where IdS = " + i;
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
                            scene.Accessibilite = reader.GetString(2);
                            scene.LieuId = reader.GetInt32(5);

                            list_Scene.Add(scene);
                        }
                    }
                }
            }

            return list_Scene;
        }

        public int Return_IdScene(string nom)
        {
            int Id = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select IdS from Scene where Nom = " + nom;
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

        public void InsertScene(int FestivalId, string Nom, string Adresse, int Capacite, bool Accessibilite, string lieu, DateTime dateJour, string artiste )
        {
            SceneDAO sceneDAO = new SceneDAO();
            LieuDAO lieuDAO = new LieuDAO();
            JourDAO jourDAO = new JourDAO();
            ArtisteDAO artisteDAO = new ArtisteDAO();
            int LieuId = lieuDAO.Return_IdLieu(lieu);
            int ArtisteId = artisteDAO.Return_IdArtiste(artiste);
            int JourId = jourDAO.Id_Jour(dateJour, FestivalId); 
            

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQueryScene = "INSERT INTO Scene (IdS, Nom, Adresse, Capacite, Accessibilite, LieuId) " +
                    "Values (IdS.NextVal, @Nom, @Adresse, @Capacite, @Accessibilite, @LieuId)";
                string sqlQueryFestivalArtiste = "INSERT INTO Festival_Artiste (Id, FestivalId, ArtisteId, SceneId, JourId) " +
                    "Values (Id.NextVal, @FestivalId, @ArtisteId, @SceneId, @JourId)";

                SqlCommand commandScene = new SqlCommand(sqlQueryScene, connection);
                commandScene.Parameters["@Nom"].Value = Nom;
                commandScene.Parameters["@Adresse"].Value = Adresse;
                commandScene.Parameters["@Capacite"].Value = Capacite;
                commandScene.Parameters["@Accessibilite"].Value = Accessibilite;
                commandScene.Parameters["@LieuId"].Value = LieuId;

                connection.Open();
                commandScene.ExecuteNonQuery();
                int SceneId = sceneDAO.Return_IdScene(Nom);

                SqlCommand commandFestivalArtist = new SqlCommand(sqlQueryFestivalArtiste, connection);
                commandScene.Parameters["@FestivalId"].Value = FestivalId;
                commandScene.Parameters["@ArtisteId"].Value = ArtisteId;
                commandScene.Parameters["@SceneId"].Value = SceneId;
                commandScene.Parameters["@JourId"].Value = JourId;

                commandFestivalArtist.ExecuteNonQuery();
                connection.Close();


            }
        }

    }
}
