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
    /// Logique d'interaction pour ModificationFestival.xaml
    /// </summary>
    public partial class ModificationFestival : Window
    {
        private Dictionary<int, string> communeLieuId = new Dictionary<int, string>();
        private readonly ICollection<Lieu> ListeLieu = new List<Lieu>();
        private readonly ICollection<Region> ListeRegion;
        private readonly Festival festivalAModifier;
        private readonly Lieu lieuFestival;
        private string path;
        public ModificationFestival(Festival festival)
        {
            InitializeComponent();

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

        private async void ModifierOnClick(object sender, RoutedEventArgs e)
        {
            string nom = nomTextBox.Text;
            string logo = path;
            string descriptif = descriptionTextBox.Text;

            if (nom.Length == 0 || descriptif.Length == 0 || lieuComboBox.SelectedItem == null ||
                dateDebutControl.SelectedDate == null || dateFinControl.SelectedDate == null)
            {
                MessageBox.Show("Veuillez remplir toutes les informations requises");
            }
            else
            {
                DateTime dateDebut = dateDebutControl.SelectedDate.Value;
                DateTime dateFin = dateFinControl.SelectedDate.Value;
                string communePrincipale = lieuComboBox.SelectedItem.ToString();
                if (dateDebut <= dateFin)
                {
                    Festival festival = festivalAModifier;
                    festival.Nom = nom;
                    festival.Descriptif = descriptif;
                    festival.Date_Debut = dateDebut;
                    festival.Date_Fin = dateFin;
                    festival.LieuId = NomCommuneToId(communePrincipale);

                    System.Net.Http.HttpResponseMessage response = await API.API.Instance.ModifFestivalAsync(festival);
                    _ = MessageBox.Show("Modification du festival en cours");
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Festival modifié");
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la modification du festival");
                    }
                }
                else
                {
                    MessageBox.Show("La période entrée n'est pas valide. Veuillez mettre les dates dans le bon ordre");
                }
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
            /*
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
            */
        }
    }
}
