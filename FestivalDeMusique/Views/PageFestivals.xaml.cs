using FestivalAPI.Models;
using FestivalDeMusique.View;
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
    /// Logique d'interaction pour PageFestivals.xaml
    /// </summary>
    public partial class PageFestivals : Page
    {
        private ICollection<Festival> ListeFestival;
        public PageFestivals()
        {
            InitializeComponent();
            Reload();
            DeactivateButtons();
        }

        private void CreerOnClick(object sender, RoutedEventArgs e)
        {
            CreerFestival creerFestival = new CreerFestival();
            creerFestival.ShowDialog();
            Reload();
            DeactivateButtons();
        }

        private void AnnulerOnClick(object sender, RoutedEventArgs e)
        {
            Festival festival = festivalGrid.SelectedItem as Festival;
            _ = API.API.Instance.SupprFestivalAsync(festival.IdF);
            Reload();
            DeactivateButtons();
            
        }

        private void ModifierOnClick(object sender, RoutedEventArgs e)
        {
            Festival festivalAModifier = festivalGrid.SelectedItem as Festival;
            ModifierFestival modifierFestival = new ModifierFestival(festivalAModifier);
            _ = modifierFestival.ShowDialog();
            DeactivateButtons();
        }

        private void Reload()
        {
            ListeFestival = API.API.Instance.GetFestivalsAsync().Result;
            festivalGrid.DataContext = ListeFestival;
        }

        private void FestivalGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActivateButtons();
            /*
            try
            {
                Festival festival = festivalGrid.SelectedItem as Festival;
                MessageBox.Show(festival.IdF.ToString());
            }
            catch
            {
                MessageBox.Show("Error while getting the value of the festival");
            }
            */
        }

        private void DeactivateButtons()
        {
            ModifierButton.IsEnabled = false;
        }

        private void ActivateButtons()
        {
            ModifierButton.IsEnabled = true;
        }

        private void RechargerPage(object sender, RoutedEventArgs e)
        {
            Reload();
        }
    }
}
