﻿using FestivalAPI.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FestivalAPI.Data
{
    public class TarifDAO
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FestivalAPIContext-adf8fbfd-4c0b-47bf-a2e7-41bddfef421b;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Tarif valeur_tarif(int IdJ)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select * from Tarif where IdJ = " + IdJ;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = IdJ;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Tarif tarif = new Tarif();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tarif.Coefficient = reader.GetInt32(0);
                        tarif.IdJ = reader.GetInt32(1);
                        tarif.IdT = reader.GetInt32(2);
                        tarif.Montant = reader.GetDouble(3);
                        tarif.Type_Tarif = reader.GetString(4);
                    }
                }

                return tarif;
            }
        }
    }
}
