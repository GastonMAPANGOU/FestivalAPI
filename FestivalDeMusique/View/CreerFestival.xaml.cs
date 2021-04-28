using FestivalAPI.Models;
using Microsoft.Win32;
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
using Path = System.IO.Path;

namespace FestivalDeMusique.View
{
    /// <summary>
    /// Logique d'interaction pour CreerFestival.xaml
    /// </summary>
    public partial class CreerFestival : Window
    {
        private Dictionary<string, int> communeLieuId = new Dictionary<string, int>();
        private readonly ICollection<Lieu> ListeLieu;
        private string path;
        public CreerFestival()
        {
            InitializeComponent();
            ListeLieu = API.API.Instance.GetLieuxAsync().Result;

            InitializeMap();
            FillComboBox();
        }

        private async void AjouterOnClick(object sender, RoutedEventArgs e)
        {
            string nom = nomTextBox.Text;
            string logo = path;
            string descriptif = descriptionTextBox.Text;

            if (nom.Length == 0 || logo.Length == 0 || 
                descriptif.Length == 0 || lieuComboBox.SelectedItem == null || 
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
                    Festival festival = new Festival();
                    festival.Nom = nom;
                    festival.Logo = logo;
                    festival.Descriptif = descriptif;
                    festival.Date_Debut = dateDebut;
                    festival.Date_Fin = dateFin;
                    festival.LieuId = NomCommuneToId(communePrincipale);

                    try
                    {
                        festival.Logo = SaveImage(festival);
                    }
                    catch
                    {
                        // To do
                    }

                    HttpResponseMessage response = await API.API.Instance.AjoutFestivalAsync(festival);
                    _ = MessageBox.Show("Création du festival en cours");
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Festival créé");
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la création du festival");
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

        private int NomCommuneToId(String commune)
        {
            return communeLieuId[commune];
        }

        private void InitializeMap()
        {
            foreach(Lieu lieu in ListeLieu)
            {
                communeLieuId.Add(lieu.Commune, lieu.IdL);
            }
        }

        private void FillComboBox()
        {
            foreach (Lieu lieu in ListeLieu)
            {
                lieuComboBox.Items.Add(lieu.Commune);
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
                
                BitmapImage bitmap= new BitmapImage(new Uri(path));
                imageUI.Source = bitmap;
            }

        }

        private String SaveImage(Festival festival)
        {
            //BitmapImage bitmap = new BitmapImage(new Uri(festival.Logo));
            //bitmap.Save("");
            using (FileStream file = File.Open(festival.Logo, FileMode.Open))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                int taillemax = 2097152;

                List<String> extensionsvalides = new List<String>();
                List<String> strcut = new List<String>();
                extensionsvalides.Add(".jpg");
                extensionsvalides.Add(".jpeg");
                extensionsvalides.Add(".gif");
                extensionsvalides.Add(".png");
                extensionsvalides.Add(".jfif");


                string fileName = "img/festivals/" + festival.Nom;
                string extension = Path.GetExtension(file.Name);
                Console.WriteLine("Extension" + extension);
                string chemin = fileName + extension.ToLower();
                Console.WriteLine("Chemin du fichier" + chemin);
                if (file.Length < taillemax && extensionsvalides.Contains(extension))
                {
                    using (FileStream fileStream = System.IO.File.Create("wwwroot/" + chemin))
                    {
                        Console.WriteLine("Enregistrement");
                        file.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    festival.Logo = chemin;
                }

            }

            //bitmap.Save(fileName);
            return festival.Logo;
                           
        }
    }
}
