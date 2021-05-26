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
            try { Reload(); } catch { MessageBox.Show("Connection à la base de donnée impossible"); }
            InitComboBox();
        }

        private void CreerOnClick(object sender, RoutedEventArgs e)
        {
            AjoutFestival ajoutFestival = new AjoutFestival();
            ajoutFestival.ShowDialog();
            Reload();
        }

        private void AnnulerOnClick(object sender, RoutedEventArgs e)
        {
            Festival festival = festivalGrid.SelectedItem as Festival;
            _ = API.API.Instance.SupprFestivalAsync(festival.IdF);
            Reload();
        }

        private void ModifierOnClick(object sender, RoutedEventArgs e)
        {
            if(festivalGrid.SelectedItem != null)
            {
                Festival festivalAModifier = festivalGrid.SelectedItem as Festival;
                ModificationFestival modificationFestival = new ModificationFestival(festivalAModifier);
                Reload();
                _ = modificationFestival.ShowDialog();
            }
            else
            {
                MessageBox.Show("Sélectionner le festival à modifier avant de cliquer sur ce bouton");
            }
            
        }

        private void Reload()
        {
            ListeFestival = API.API.Instance.GetFestivalsAsync().Result;
            festivalGrid.DataContext = ListeFestival;
        }

        private void FestivalGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
                Reload();
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
                    try
                    {
                        ICollection<Festival> listfest = API.API.Instance.GetFestivalsAsync().Result;
                        ListeFestival = new List<Festival>();
                        foreach (Festival fest in listfest)
                        {
                            if (fest.Nom.ToLower().Contains(recherche))
                            {
                                ListeFestival.Add(fest);
                            }
                        }
                        festivalGrid.DataContext = ListeFestival;
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    
                }
                else
                {
                    try
                    {
                        ICollection<Festival> listfest = API.API.Instance.GetFestivalsAsync().Result;
                        ListeFestival = new List<Festival>();
                        foreach (Festival fest in listfest)
                        {
                            if (fest.Descriptif.ToLower().Contains(recherche))
                            {
                                ListeFestival.Add(fest);
                            }
                        }
                        festivalGrid.DataContext = ListeFestival;
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }

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
