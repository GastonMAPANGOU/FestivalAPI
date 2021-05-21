using System;
using System.Collections.Generic;
using System.IO;
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
using FestivalAPI.Models;
using Microsoft.Win32;

namespace FestivalDeMusique.Views
{
    /// <summary>
    /// Logique d'interaction pour ConsulterFestival.xaml
    /// </summary>
    public partial class ConsulterFestival : Window
    {
        private Dictionary<int, string> communeLieuId = new Dictionary<int, string>();
        private readonly ICollection<Lieu> ListeLieu = new List<Lieu>();
        private readonly ICollection<Region> ListeRegion;
        private readonly Festival festivalAModifier;
        private readonly Lieu lieuFestival;
        private string path;
        public ConsulterFestival(Festival festival)
        {
            InitializeComponent();
            try
            {
                festivalAModifier = festival;
                lieuFestival = API.API.Instance.GetLieuAsync(festivalAModifier.LieuId).Result;

                ListeRegion = API.API.Instance.GetRegionsAsync().Result;
                foreach (Region region in ListeRegion)
                {
                    if (region.Nom.Trim().ToLower().Equals("normandie"))
                    {
                        foreach (Departement departement in region.Departements)
                        {
                            Departement dep = API.API.Instance.GetDepartementAsync(departement.Id).Result;
                            foreach (Lieu lieu in dep.Lieux)
                            {
                                if (!ListeLieu.Contains(lieu))
                                    ListeLieu.Add(lieu);
                            }
                        }
                    }
                }

                path = festival.Logo;

                InitializeMap();
                FillComboBox();
                FillFestivalData();
            }
            catch
            {
                Close();
            }
            
        }

        private void ModifierOnClick(object sender, RoutedEventArgs e)
        {
            
            Festival festival = festivalAModifier;
            EnvoyerEmail envoyerEmail = new EnvoyerEmail(festival);
            envoyerEmail.ShowDialog();
            bool IsCanceled = envoyerEmail.IsCanceled;
            if (IsCanceled)
            {
                MessageBox.Show("Festival annulé");
            }
            else
            {
                MessageBox.Show("Erreur lors de l'annulation du festival");
            }

        }

        private void RetourOnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void AnnulerOnClick(object sender, RoutedEventArgs e)
        {
            //FillFestivalData();
            Close();
        }

        private int NomCommuneToId(String commune)
        {
            int returnValue = 0;
            foreach (KeyValuePair<int, string> leLieu in communeLieuId)
            {
                if (leLieu.Value.Equals(commune))
                    returnValue = leLieu.Key;
            }
            return returnValue;
        }

        private void InitializeMap()
        {
            foreach (Lieu lieu in ListeLieu)
            {
                communeLieuId.Add(lieu.IdL, lieu.Commune);
            }
        }

        private void FillComboBox()
        {
            foreach (Lieu lieu in ListeLieu)
            {
                lieuComboBox.Items.Add(lieu.Commune);
            }
        }

        private void FillFestivalData()
        {
            nomTextBox.Text = festivalAModifier.Nom;
            descriptionTextBox.Text = festivalAModifier.Descriptif;
            dateDebutControl.SelectedDate = festivalAModifier.Date_Debut;
            dateFinControl.SelectedDate = festivalAModifier.Date_Fin;
            lieuComboBox.SelectedItem = lieuFestival.Commune;
            try
            {
                imageUI.Source = new BitmapImage(new Uri(festivalAModifier.Logo));
            }
            catch
            {
                path = "";
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Choisissez votre logo";
            op.Filter = "Format compatibles|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                path = op.FileName;
                imageUI.Source = new BitmapImage(new Uri(path));
            }
        }

        private void imageUI_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Choisissez votre logo";
            op.Filter = "Format compatibles|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                path = op.FileName;
                imageUI.Source = new BitmapImage(new Uri(path));
            }
        }
    }
}
