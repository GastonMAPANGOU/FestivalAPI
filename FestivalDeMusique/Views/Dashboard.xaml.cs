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
    /// Logique d'interaction pour Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        private readonly Gimi gimi;
        public Dashboard()
        {
            InitializeComponent();
            frame.NavigationService.Navigate(new PageFestivals());
        }

        public Dashboard(Gimi gimi)
        {
            InitializeComponent();
            frame.NavigationService.Navigate(new PageFestivals());
            this.gimi = gimi;
            ChargerGimi();
        }

        private void ChargerPageFestivals(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new PageFestivals());
        }

        private void ChargerPageOrganisateurs(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new PageOrganisateurs());
        }

        private void ChargerPageStatistiques(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new PageStatistiques());
        }

        private void ChargerPageAide(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new PageAides());
        }

        private void ChargerPageApropos(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new PageApropos());
        }

        private void ChargerPageCompte(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new PageCompte());
        }

        private void Deconnexion(object sender, RoutedEventArgs e)
        {
            Authentification authentification = new Authentification();
            authentification.Show();
            Close();
        }

        private void ChargerGimi()
        {
            compteButton.Content = "Compte : " + gimi.Nom + " " + gimi.Prenom;
        }
    }
}
