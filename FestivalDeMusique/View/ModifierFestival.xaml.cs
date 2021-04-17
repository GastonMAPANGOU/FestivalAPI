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

namespace FestivalDeMusique.View
{
    /// <summary>
    /// Logique d'interaction pour ModifierFestival.xaml
    /// </summary>
    public partial class ModifierFestival : Window
    {
        private Dictionary<string, int> communeLieuId = new Dictionary<string, int>();
        private readonly ICollection<Lieu> ListeLieu;
        private readonly Festival festivalAModifier;
        private readonly Lieu lieuFestival;
        public ModifierFestival(Festival festival)
        {
            InitializeComponent();

            festivalAModifier = festival;
            lieuFestival = API.API.Instance.GetLieuAsync(festivalAModifier.LieuId).Result;
            ListeLieu = API.API.Instance.GetLieuxAsync().Result;

            InitializeMap();
            FillComboBox();
            FillFestivalData();
        }

        private async void AjouterOnClick(object sender, RoutedEventArgs e)
        {
            string nom = nomTextBox.Text;
            string logo = logoTextBox.Text;
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
            FillFestivalData();
        }

        private int NomCommuneToId(String commune)
        {
            return communeLieuId[commune];
        }

        private void InitializeMap()
        {
            foreach (Lieu lieu in ListeLieu)
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

        private void FillFestivalData()
        {
            nomTextBox.Text = festivalAModifier.Nom;
            logoTextBox.Text = festivalAModifier.Logo;
            descriptionTextBox.Text = festivalAModifier.Descriptif;
            dateDebutControl.SelectedDate = festivalAModifier.Date_Debut;
            dateFinControl.SelectedDate = festivalAModifier.Date_Fin;
            lieuComboBox.SelectedItem = lieuFestival.Commune;
        }
    }
}
