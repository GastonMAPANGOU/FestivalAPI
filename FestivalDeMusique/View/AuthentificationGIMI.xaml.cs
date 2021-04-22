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
            string email = textBoxEmail.Text;
            string pass = passwordTextBox.Text;

            if (email.Trim().Length == 0 || pass.Trim().Length == 0)
            {
                MessageBox.Show("Veuillez remplir tous les champs");
            }
            else
            {
                pass = Hash(pass);
                if (CheckCredentials(email, pass))
                {
                    textBoxEmail.Text = "";
                    passwordTextBox.Text = "";
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
            Gimi gimi = API.API.Instance.GetGimi(email, pass).Result;
            try
            {
                return email == gimi.Login && pass == gimi.Pwd;
            }
            catch
            {
                return false;
            }
        }

        private string Hash(String mdp)
        {
            var bytes = new UTF8Encoding().GetBytes(mdp);
            var hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
