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
    /// Logique d'interaction pour ModifierOrganisateur.xaml
    /// </summary>
    public partial class ModifierOrganisateur : Window
    {
        private readonly Festival festival;
        private readonly Organisateur organisateur;
        public ModifierOrganisateur(Organisateur organisateur)
        {
            InitializeComponent();
            this.organisateur = organisateur;
            try {
                this.festival = API.API.Instance.GetFestivalAsync(organisateur.FestivalId).Result;
                ChargerOrganisateur();
            }
            catch { Close(); }
            
        }

        private void Retour_ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void Valider_ButtonClick(object sender, RoutedEventArgs e)
        {
            string nom = nomTextBox.Text;
            string prenom = prenomTextBox.Text;
            string login = emailTextBox.Text;
            string pwd = motDePasseTextBox.Text;
            bool validation = validationCheckBox.IsChecked.Value;
            if (nom.Trim().Length == 0 || prenom.Trim().Length == 0 || login.Trim().Length == 0 || pwd.Trim().Length == 0)
            {
                MessageBox.Show("Veuillez remplir tous les champs textuels");
            }
            else
            {
                Organisateur org = organisateur;
                org.Nom = nom; org.Prenom = prenom; org.Login = login; org.Pwd = pwd;
                org.InscriptionAccepted = validation;

                HttpResponseMessage response = await API.API.Instance.ModifOrganisateurAsync(org);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Organisateur modifié avec succès");
                    Close();
                }
                else
                {
                    MessageBox.Show("Erreur lors de la modification de l'organisateur");
                }
            }
        }

        private void ChargerOrganisateur()
        {
            nomTextBox.Text = organisateur.Nom;
            prenomTextBox.Text = organisateur.Prenom;
            emailTextBox.Text = organisateur.Login;
            motDePasseTextBox.Text = organisateur.Pwd;
            validationCheckBox.IsChecked = organisateur.InscriptionAccepted;
            FestivalTextBlock.Text = festival.Nom;
        }
    }
}
