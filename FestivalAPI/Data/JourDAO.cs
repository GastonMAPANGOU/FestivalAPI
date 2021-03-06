using FestivalAPI.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Data
{
    public class JourDAO
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FestivalAPIContext-adf8fbfd-4c0b-47bf-a2e7-41bddfef421b;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public int Id_Jour(DateTime dateJour, int FestivalId)
        {
            Jour jour = new Jour();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select IdJ from dbo.Jour where Date_Jour = " + dateJour + " And FestivalId = " +FestivalId;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        jour.IdJ = reader.GetInt32(0);
                    }                
                }
            }
            return jour.IdJ;
        }

        public Jour Return_Jour(int JourId, int FestivalId)
        {
            Jour jour = new Jour();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select * from dbo.Jour where IdJ = "+JourId+" and FestivalId = "+FestivalId;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        jour.IdJ = reader.GetInt32(0);
                        jour.Numero_jour = reader.GetString(1);
                        jour.Date_jour = reader.GetDateTime(2);
                        jour.FestivalId = reader.GetInt32(3);
                    }
                }
            }
            return jour;
        }
    }
}
