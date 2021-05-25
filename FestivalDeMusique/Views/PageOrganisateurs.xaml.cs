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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FestivalDeMusique.Views
{
    /// <summary>
    /// Logique d'interaction pour PageOrganisateurs.xaml
    /// </summary>
    public partial class PageOrganisateurs : Page
    {
        private ICollection<Organisateur> ListeOrganisateurs;
        public PageOrganisateurs()
        {
            InitializeComponent();
            Reload();
        }

        private void Reload()
        {
            ListeOrganisateurs = API.API.Instance.GetOrganisateursAsync().Result;
            organisateurGrid.DataContext = ListeOrganisateurs;
        }

        private void OrganisateurGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void RechargerPage(object sender, RoutedEventArgs e)
        {
            Reload();
        }

        private async void ValiderOrganisateur(object sender, RoutedEventArgs e)
        {
            try
            {
                Organisateur org = organisateurGrid.SelectedItem as Organisateur;
                org.InscriptionAccepted = true;
                System.Net.Http.HttpResponseMessage response = await API.API.Instance.ModifOrganisateurAsync(org);
                Reload();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Sélectionnez un organisateur à valider");
            }
        }

        private async void Supprimer_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Organisateur org = organisateurGrid.SelectedItem as Organisateur;
                System.Net.Http.HttpResponseMessage response = await API.API.Instance.SupprOrganisateurAsync(org.IdO);
                Reload();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sélectionnez un organisateur à supprimer");
            }
        }

        private void DataGridCheckBoxColumn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Hello world");
        }
    }
}
