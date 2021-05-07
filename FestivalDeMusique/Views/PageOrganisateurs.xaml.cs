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
            /*
            try
            {
                Organisateur organisateur = organisateurGrid.SelectedItem as Organisateur;
                MessageBox.Show(organisateur.IdO.ToString());
            }
            catch
            {
                MessageBox.Show("Error while getting the value of the organisateur");
            }
            */
        }

        private void RechargerPage(object sender, RoutedEventArgs e)
        {
            Reload();
        }
    }
}
