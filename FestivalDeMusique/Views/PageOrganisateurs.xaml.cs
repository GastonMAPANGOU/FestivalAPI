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
            InitComboBox();
            Reload();
        }

        private void Reload()
        {
            ListeOrganisateurs = API.API.Instance.GetOrganisateursAsync().Result;
            organisateurGrid.DataContext = ListeOrganisateurs;
        }

        private void OrganisateurGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void RechargerPage(object sender, RoutedEventArgs e)
        {
            Reload();
        }

        private async void ValiderOrganisateur(object sender, RoutedEventArgs e)
        {
            try
            {
                Organisateur org = organisateurGrid.SelectedItem as Organisateur;
                org.InscriptionAccepted = true;
                System.Net.Http.HttpResponseMessage response = await API.API.Instance.ModifOrganisateurAsync(org);
                Reload();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Sélectionnez un organisateur à valider");
            }
        }

        private async void Supprimer_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Organisateur org = organisateurGrid.SelectedItem as Organisateur;
                System.Net.Http.HttpResponseMessage response = await API.API.Instance.SupprOrganisateurAsync(org.IdO);
                Reload();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sélectionnez un organisateur à supprimer");
            }
        }

        private void DataGridCheckBoxColumn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Hello world");
        }

        private void RechercheTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string critere = (string) RechercheComboBox.SelectedItem;
            string recherche = RechercheTextBox.Text.ToLower();

            if(recherche.Trim().Length == 0)
            {
                Reload();
            }
            else
            {
                if(critere == "Nom") 
                {
                    try
                    {
                        ICollection<Organisateur> listOrg = API.API.Instance.GetOrganisateursAsync().Result;
                        ListeOrganisateurs = new List<Organisateur>();
                        foreach (Organisateur org in listOrg)
                        {
                            if (org.Nom.ToLower().Contains(recherche))
                            {
                                ListeOrganisateurs.Add(org);
                            }
                        }
                        organisateurGrid.DataContext = ListeOrganisateurs;
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    
                }
                else if(critere == "Prénom") 
                {
                    try
                    {
                        ICollection<Organisateur> listOrg = API.API.Instance.GetOrganisateursAsync().Result;
                        ListeOrganisateurs = new List<Organisateur>();
                        foreach (Organisateur org in listOrg)
                        {
                            if (org.Prenom.ToLower().Contains(recherche))
                            {
                                ListeOrganisateurs.Add(org);
                            }
                        }
                        organisateurGrid.DataContext = ListeOrganisateurs;
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
                else 
                {
                    try
                    {
                        ICollection<Organisateur> listOrg = API.API.Instance.GetOrganisateursAsync().Result;
                        ListeOrganisateurs = new List<Organisateur>();
                        foreach (Organisateur org in listOrg)
                        {
                            if (org.Login.ToLower().Contains(recherche))
                            {
                                ListeOrganisateurs.Add(org);
                            }
                        }
                        organisateurGrid.DataContext = ListeOrganisateurs;
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
        }

        private void InitComboBox()
        {
            RechercheComboBox.Items.Add("Nom");
            RechercheComboBox.Items.Add("Prénom");
            RechercheComboBox.Items.Add("E-mail");
            RechercheComboBox.SelectedIndex = 0;
        }
    }
}
