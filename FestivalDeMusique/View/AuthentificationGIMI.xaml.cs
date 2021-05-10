using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FestivalAPI.Models;

namespace FestivalDeMusique.View
{
    /// <summary>
    /// Logique d'interaction pour AuthentificationGIMI.xaml
    /// </summary>
    public partial class AuthentificationGIMI : Window
    {
        public AuthentificationGIMI()
        {
            InitializeComponent();
        }

        private void LoginFunction(object sender, RoutedEventArgs e)
        {
            string email = textBoxEmail.Text.Trim();
            string pass = passwordTextBox.Text.Trim();

            if (email.Length == 0 || pass.Length == 0)
            {
                MessageBox.Show("Veuillez remplir tous les champs");
            }
            else
            {
                if (CheckCredentials(email, pass))
                {
                    Hide();
                    MenuGestionnaire menu = new MenuGestionnaire();
                    menu.ShowDialog();
                    Show();
                }
                else
                {
                    MessageBox.Show("E-mail ou mot de passe incorrect.");
                }
            }
        }

        private bool CheckCredentials(string email, string pass)
        {
            ICollection<Gimi> liste;
            liste = API.API.Instance.GetGimis().Result;
            foreach(Gimi gimi in liste)
            {
                if(gimi.Login == email && gimi.Pwd == pass)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
