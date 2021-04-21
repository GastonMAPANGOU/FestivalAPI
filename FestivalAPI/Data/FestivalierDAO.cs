using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FestivalAPI.Models;
using Microsoft.Data.SqlClient;

namespace FestivalAPI.Data
{
    internal class FestivalierDAO
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
                        festivalier.Login = reader.GetString(3);
                        festivalier.Nb_Participants = reader.GetInt32(5);
                        festivalier.Nom = reader.GetString(1);
                        festivalier.Prenom = reader.GetString(2);
                        festivalier.Pwd = reader.GetString(4);
                        festivalier.Somme = reader.GetDouble(6);

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
                        festivalier.Login = reader.GetString(3);
                        festivalier.Nb_Participants = reader.GetInt32(5);
                        festivalier.Nom = reader.GetString(1);
                        festivalier.Prenom = reader.GetString(2);
                        festivalier.Pwd = reader.GetString(4);
                        festivalier.Somme = reader.GetDouble(6);

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
                        festivalier.Login = reader.GetString(3);
                        festivalier.Nb_Participants = reader.GetInt32(5);
                        festivalier.Nom = reader.GetString(1);
                        festivalier.Prenom = reader.GetString(2);
                        festivalier.Pwd = reader.GetString(4);
                        festivalier.Somme = reader.GetDouble(6);

                    }
                }
            }

            return festivalier;
        }




        public Festivalier Insert(DateTime dateJour, int nbr_personne, string Nom, string Prenom, String Login, string Pwd, Double somme, int FestivalId, int nbr_Jour, int coefficient)
        {
            FestivalierDAO festivalierDAO = new FestivalierDAO();
            int IdJ;
            JourDAO jourDAO = new JourDAO();
            TarifDAO tarifDAO = new TarifDAO(); 
            Tarif tarif = new Tarif();
            Festivalier festivalier = new Festivalier();

            IdJ = jourDAO.Id_Jour(dateJour, FestivalId);
            tarif = tarifDAO.valeur_tarif(IdJ, coefficient);

            somme = tarif.Montant * tarif.Coefficient * nbr_personne * nbr_Jour;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Insert into Festivalier (Id, Nom, Prenom, Login, Pwd, Nb_Participants, Somme, FestivalId) " +
                                    "Values (Id.nextVal, @Nom, @Prenom, @Login, @Pwd, @Nb_Participants, @Somme, @FestiavalId)";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters["@Nom"].Value = Nom;
                command.Parameters["@Prenom"].Value = Prenom;
                command.Parameters["@Login"].Value = Login;
                command.Parameters["@Pwd"].Value = Pwd;
                command.Parameters["@Nb_Participants"].Value = nbr_personne;
                command.Parameters["@Somme"].Value = somme;
                command.Parameters["@FestivalId"].Value = FestivalId;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                festivalier = festivalierDAO.Display_One_Festivalier_Login(Login, FestivalId);

                return festivalier;
            }
        }

        public void Update(int Id, DateTime dateJour, int nbr_personne, string Nom, string Prenom, String Login, string Pwd, Double somme, int FestivalId, int Coefficient)
        {
            int IdJ;
            JourDAO jourDAO = new JourDAO();
            TarifDAO tarifDAO = new TarifDAO();
            Tarif tarif = new Tarif();

            IdJ = jourDAO.Id_Jour(dateJour, FestivalId);
            tarif = tarifDAO.valeur_tarif(IdJ, Coefficient);

            somme = tarif.Montant * tarif.Coefficient * nbr_personne;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Update Festivalier (Id, Nom, Prenom, Login, Pwd, Nb_Participants, Somme, FestivalId) " +
                                    "Values (Id.nextVal, @Nom, @Prenom, @Login, @Pwd, @Nb_Participants, @Somme, @FestiavalId) where Id = "+ Id;

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters["@Nom"].Value = Nom;
                command.Parameters["@Prenom"].Value = Prenom;
                command.Parameters["@Login"].Value = Login;
                command.Parameters["@Pwd"].Value = Pwd;
                command.Parameters["@Nb_Participants"].Value = nbr_personne;
                command.Parameters["@Somme"].Value = somme;
                command.Parameters["@FestivalId"].Value = FestivalId;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

            }
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
