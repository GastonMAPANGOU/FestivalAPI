using FestivalAPI.Models;
using FestivalDeMusique.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FestivalDeMusique.Views
{
    /// <summary>
    /// Logique d'interaction pour Authentification.xaml
    /// </summary>
    public partial class Authentification : Window
    {
        public Authentification()
        {
            InitializeComponent();
        }

        private void Fermer_Fenetre(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Reduire_Fenetre(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
            // WindowState = WindowState.Maximized;
        }

        private void DragFrame(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void LoginFunction(object sender, RoutedEventArgs e)
        {
            string email = textBoxEmail.Text;
            string pass = passwordTextBox.Password;

            if (email.Trim().Length == 0 || pass.Trim().Length == 0)
            {
                MessageBox.Show("Veuillez remplir tous les champs");
            }
            else
            {
                //pass = Hash(pass);
                if (CheckCredentials(email, pass))
                {
                    Gimi gimi = API.API.Instance.GetGimi(email, pass).Result;
                    Dashboard menu = new Dashboard(gimi);
                    menu.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("E-mail ou mot de passe incorrect.");
                }
            }
        }

        private bool CheckCredentials(string email, string pass)
        {
            foreach(Gimi gimi in API.API.Instance.GetGimis().Result)
            {
                if (email == gimi.Login && pass == gimi.Pwd)
                    return true;
            }
            return false;
        }

        private string Hash(String mdp)
        {
            var bytes = new UTF8Encoding().GetBytes(mdp);
            var hashBytes = System.Security.Cryptography.SHA256.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }

    }
}
