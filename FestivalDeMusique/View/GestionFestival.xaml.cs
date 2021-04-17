using FestivalAPI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FestivalDeMusique.View
{
    /// <summary>
    /// Logique d'interaction pour GestionFestival.xaml
    /// </summary>
    public partial class GestionFestival : Window
    {
        private ICollection<Festival> ListeFestival;
        public GestionFestival()
        {
            InitializeComponent();
            Reload();
            DeactivateButtons();
        }

        private void CreerOnClick(object sender, RoutedEventArgs e)
        {
            Hide();
            CreerFestival creerFestival = new CreerFestival();
            creerFestival.ShowDialog();
            Reload();
            DeactivateButtons();
            _ = ShowDialog();
        }
        
        private void AnnulerOnClick(object sender, RoutedEventArgs e)
        {
            Festival festivalAModifier = (Festival)festivalGrid.SelectedItem;
            _ = API.API.Instance.SupprFestivalAsync(festivalAModifier.IdF);
            Reload();
            DeactivateButtons();
            EnvoyerMailAnnulation(festivalAModifier);
        }

        private void ModifierOnClick(object sender, RoutedEventArgs e)
        {
            Festival festivalAModifier = (Festival)festivalGrid.SelectedItem;
            Hide();
            ModifierFestival modifierFestival = new ModifierFestival(festivalAModifier);
            _ = modifierFestival.ShowDialog();
            DeactivateButtons();
            _ = ShowDialog();
        }

        private void Reload()
        {
            ListeFestival = API.API.Instance.GetFestivalsAsync().Result;
            festivalGrid.DataContext = ListeFestival;
        }

        private void FestivalGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActivateButtons();
        }

        private void DeactivateButtons()
        {
            ModifierButton.IsEnabled = false;
            AnnulerButton.IsEnabled = false;
        }

        private void ActivateButtons()
        {
            ModifierButton.IsEnabled = true;
            AnnulerButton.IsEnabled = true;
        }

        private void EnvoyerMailAnnulation(Festival festival)
        {
            // To do
        }

        private void RechargerPage(object sender, RoutedEventArgs e)
        {
            Reload();
        }
    }
}
