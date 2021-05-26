using FestivalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    /// Logique d'interaction pour AjoutOrganisateur.xaml
    /// </summary>
    public partial class AjoutOrganisateur : Window
    {
        private readonly Festival festival;
        public AjoutOrganisateur(Festival festival)
        {
            InitializeComponent();
            this.festival = festival;
            FestivalTextBlock.Text = festival.Nom;
        }

        private void Retour_ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void Ajouter_ButtonClick(object sender, RoutedEventArgs e)
        {
            string nom = nomTextBox.Text;
            string prenom = prenomTextBox.Text;
            string login = emailTextBox.Text;
            string pwd = motDePasseTextBox.Text;
            bool validation = validationCheckBox.IsChecked.Value;
            if(nom.Trim().Length == 0 || prenom.Trim().Length == 0 || login.Trim().Length == 0 || pwd.Trim().Length == 0)
            {
                MessageBox.Show("Veuillez remplir tous les champs textuels");
            }
            else
            {
                Organisateur organisateur = new Organisateur()
                {
                    Nom = nom, Prenom = prenom, FestivalId = festival.IdF, InscriptionAccepted = validation, 
                    Login = login, Pwd = pwd
                };

                HttpResponseMessage response = await API.API.Instance.AjoutOrganisateurAsync(organisateur);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Nouvel organisateur ajouté avec succès");
                    nomTextBox.Text = "";
                    prenomTextBox.Text = "";
                    emailTextBox.Text = "";
                    motDePasseTextBox.Text = "";
                    validationCheckBox.IsChecked = false;
                }
                else
                {
                    MessageBox.Show("Erreur lors de l'ajout de l'organisateur");
                }
            }
        }
    }
}
