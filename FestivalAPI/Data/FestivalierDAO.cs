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
                        festivalier.Login = reader.GetString(1);
                        festivalier.Nb_Participants = reader.GetInt32(2);
                        festivalier.Nom = reader.GetString(3);
                        festivalier.Prenom = reader.GetString(4);
                        festivalier.Pwd = reader.GetString(5);
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
                        festivalier.Login = reader.GetString(1);
                        festivalier.Nb_Participants = reader.GetInt32(2);
                        festivalier.Nom = reader.GetString(3);
                        festivalier.Prenom = reader.GetString(4);
                        festivalier.Pwd = reader.GetString(5);
                        festivalier.Somme = reader.GetDouble(6);
                    }
                }
            }
   
            return festivalier;
        }

        public void Insert(DateTime dateJour, int nbr_personne, string Nom, string Prenom, String Login, string Pwd, Double somme, int FestivalId, int nbr_jour)
        {
            int IdJ;
            JourDAO jourDAO = new JourDAO();
            TarifDAO tarifDAO = new TarifDAO(); 
            Tarif tarif = new Tarif();

            IdJ = jourDAO.Id_Jour(dateJour);
            tarif = tarifDAO.valeur_tarif(IdJ);

            somme = tarif.Montant * tarif.Coefficient * nbr_personne * nbr_jour;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Insert into Festivalier (Nom, Prenom, Login, Pwd, Nb_Participants, Somme, FestivalId) " +
                                    "Values (@Nom, @Prenom, @Login, @Pwd, @Nb_Participants, @Somme, @FestiavalId)";

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

        public void Update(int Id, DateTime dateJour, int nbr_personne, string Nom, string Prenom, String Login, string Pwd, Double somme, int FestivalId)
        {
            int IdJ;
            JourDAO jourDAO = new JourDAO();
            TarifDAO tarifDAO = new TarifDAO();
            Tarif tarif = new Tarif();

            IdJ = jourDAO.Id_Jour(dateJour);
            tarif = tarifDAO.valeur_tarif(IdJ);

            somme = tarif.Montant * tarif.Coefficient * nbr_personne;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Update Festivalier (Nom, Prenom, Login, Pwd, Nb_Participants, Somme, FestivalId) " +
                                    "Values (@Nom, @Prenom, @Login, @Pwd, @Nb_Participants, @Somme, @FestiavalId) where Id = "+ Id;

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
