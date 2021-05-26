using FestivalAPI.Models;
using System;
using System.Collections.Generic;
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

namespace FestivalDeMusique.Views
{
    /// <summary>
    /// Logique d'interaction pour EnvoyerEmail.xaml
    /// </summary>
    public partial class EnvoyerEmail : Window
    {
        private readonly ICollection<Festivalier> ListeFestivaliers;
        private readonly ICollection<Organisateur> ListeOrganisateurs;
        private FestivalAPI.Data.SendMail sendMail;
        private Festival festival;
        public bool IsCanceled = false;
        
        public EnvoyerEmail()
        {
            InitializeComponent();
            sendMail = new FestivalAPI.Data.SendMail();
        }

        public EnvoyerEmail(Festival festival)
        {
            InitializeComponent();
            sendMail = new FestivalAPI.Data.SendMail();
            this.festival = festival;

            try
            {
                ICollection<Organisateur> listOrg = API.API.Instance.GetOrganisateursAsync().Result;
                ListeOrganisateurs = new List<Organisateur>();
                foreach (Organisateur org in listOrg)
                {
                    if (org.FestivalId == festival.IdF)
                    {
                        ListeOrganisateurs.Add(org);
                    }
                }
                
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            try
            {
                ICollection<Festivalier> listeFest = API.API.Instance.GetFestivaliersAsync().Result;
                ListeFestivaliers = new List<Festivalier>();
                foreach (Festivalier festivalier in listeFest)
                {
                    if (festivalier.FestivalId == festival.IdF)
                    {
                        ListeFestivaliers.Add(festivalier);
                    }
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void AnnulerButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void ValiderButton_Click(object sender, RoutedEventArgs e)
        {
            string content = MailContent.Text;
            string mailSubject = MailSubject.Text;

            if(mailSubject.Trim().Length == 0 || content.Trim().Length == 0)
            {
                MessageBox.Show("Veuillez remplir tous les champs.");
            }
            else
            {
                Festival festivalA = festival;
                festivalA.IsCanceled = true;
                HttpResponseMessage response = await API.API.Instance.ModifFestivalAsync(festivalA);
                if (response.IsSuccessStatusCode)
                {
                    IsCanceled = true;
                    EnvoyerMailFestivaliers(mailSubject, content);
                    EnvoyerMailOrganisateurs(mailSubject);
                    Close();
                }
                else
                {
                    MessageBox.Show("Erreur lors de l'annulation du festival");
                }
                
            }
        }

        
        private void EnvoyerMailFestivaliers(string mailSubject, string content)
        {
            string sendMailTo = "";
            foreach ( Festivalier festivalier in ListeFestivaliers)
            {
                sendMailTo = festivalier.Login;
                sendMail.ActionSendMail(sendMailTo, mailSubject, content);
            }
            MessageBox.Show("Message envoyé aux festivaliers du festival : " + festival.Nom);
        }

        private void EnvoyerMailOrganisateurs(string mailSubject)
        {
            string sendMailTo = "";
            foreach (Organisateur organisateur in ListeOrganisateurs)
            {
                sendMailTo = organisateur.Login;
                string contenu = "Monsieur/Madame " + organisateur.Nom + " " + organisateur.Prenom;
                contenu += "<br> <br>Le festival" + festival.Nom + " dont vous étiez organisateur est annulé";
                contenu += "<br> <br> Cordialement, <br> <br> A bientot sur Festi'Normandie";
                sendMail.ActionSendMail(sendMailTo, mailSubject, contenu);
            }
            MessageBox.Show("Message envoyé aux organisateurs du festival : " + festival.Nom);
        }
    }
}
