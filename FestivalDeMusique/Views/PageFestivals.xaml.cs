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
            try { Reload(); } catch { }
            DeactivateButtons();
            InitComboBox();
        }

        private void CreerOnClick(object sender, RoutedEventArgs e)
        {
            AjoutFestival ajoutFestival = new AjoutFestival();
            ajoutFestival.ShowDialog();
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
            ModificationFestival modificationFestival = new ModificationFestival(festivalAModifier);
            _ = modificationFestival.ShowDialog();
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

        private void festivalGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Festival festivalA = festivalGrid.SelectedItem as Festival;
                ConsulterFestival modificationFestival = new ConsulterFestival(festivalA);
                modificationFestival.Show();
            }
            catch(Exception ex)
            {
                
            }
        }

        private void RechercheTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string critere = RechercheComboBox.SelectedItem.ToString();
            string recherche = RechercheTextBox.Text.ToLower();

            if (recherche.Trim().Length == 0)
            {
                Reload();
            }
            else
            {
                if (critere == "Nom")
                {
                    ListeFestival = (ICollection<Festival>)API.API.Instance.GetFestivalsAsync().Result.Where(fest => fest.Nom.ToLower().Contains(recherche));
                }
                else
                {
                    ListeFestival = (ICollection<Festival>)API.API.Instance.GetFestivalsAsync().Result.Where(org => org.Descriptif.ToLower().Contains(recherche));
                }
            }
        }

        private void InitComboBox()
        {
            RechercheComboBox.Items.Add("Nom");
            RechercheComboBox.Items.Add("Descriptif");
            RechercheComboBox.SelectedIndex = 0;
        }
    }
}
