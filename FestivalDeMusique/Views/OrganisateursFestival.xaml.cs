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
            try {
                ICollection<Organisateur> listOrg = API.API.Instance.GetOrganisateursAsync().Result;
                ListeOrganisateurs = new List<Organisateur>();
                foreach (Organisateur org in listOrg)
                {
                    if(org.FestivalId == festival.IdF)
                    {
                        ListeOrganisateurs.Add(org);
                    }
                }
                OrganisateursGrid.DataContext = ListeOrganisateurs;
            }
            catch { }
            
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
            AjoutOrganisateur ajoutOrganisateur = new AjoutOrganisateur(festival);
            ajoutOrganisateur.ShowDialog();
            Reload();
        }

        private void Modifier_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Organisateur org = OrganisateursGrid.SelectedItem as Organisateur;
                ModifierOrganisateur modifierOrganisateur = new ModifierOrganisateur(org);
                modifierOrganisateur.ShowDialog();
                Reload();
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
