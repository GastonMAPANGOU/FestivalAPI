using FestivalAPI.Models;
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
    /// Logique d'interaction pour OrganisateursFestival.xaml
    /// </summary>
    public partial class OrganisateursFestival : Window
    {
        private ICollection<Organisateur> ListeOrganisateurs;
        private readonly Festival festival;
        public OrganisateursFestival(Festival festival)
        {
            InitializeComponent();
            this.festival = festival;
            NomFestival.Text = festival.Nom;
            Reload();
        }

        private void Reload()
        {
            ListeOrganisateurs = (ICollection<Organisateur>)API.API.Instance.GetOrganisateursAsync().Result.Where(org => org.FestivalId == festival.IdF);
            OrganisateursGrid.DataContext = ListeOrganisateurs;
        }

        private void OrganisateursGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrganisateursGrid.SelectedItem != null)
                ButtonOn();
            else
                ButtonOff();
        }

        private void RechargerPage(object sender, RoutedEventArgs e)
        {
            Reload();
        }

        private void Ajouter_ButtonClick(object sender, RoutedEventArgs e)
        {
            // Windows to add organizer to the festival, takes festival as parameter
        }

        private void Modifier_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Organisateur org = OrganisateursGrid.SelectedItem as Organisateur;
                // Open a window to change details about the organizer
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sélectionnez l'organisateur à modifier");
            }
        }

        private async void Supprimer_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Organisateur org = OrganisateursGrid.SelectedItem as Organisateur;
                System.Net.Http.HttpResponseMessage response = await API.API.Instance.SupprOrganisateurAsync(org.IdO);
                Reload();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sélectionnez l'organisateur à supprimer");
            }
        }

        private void Fermer_ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void ButtonOff()
        {
            ModifierButton.IsEnabled = false;
            SupprimerButton.IsEnabled = false;
        }

        private void ButtonOn()
        {
            ModifierButton.IsEnabled = true;
            SupprimerButton.IsEnabled = true;
        }
    }
}
