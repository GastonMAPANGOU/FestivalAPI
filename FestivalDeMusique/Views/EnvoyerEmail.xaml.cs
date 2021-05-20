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

namespace FestivalDeMusique.Views
{
    /// <summary>
    /// Logique d'interaction pour EnvoyerEmail.xaml
    /// </summary>
    public partial class EnvoyerEmail : Window
    {
        //private readonly ICollection<Festivalier> listeFestivaliers;
        //private readonly Organisateur organisateur;
        private FestivalAPI.Data.SendMail sendMail;
        private readonly Festival festival;

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
        }

        private void AnnulerButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ValiderButton_Click(object sender, RoutedEventArgs e)
        {
            string content = MailContent.Text;
            string mailSubject = MailSubject.Text;

            if(mailSubject.Trim().Length == 0 || content.Trim().Length == 0)
            {
                MessageBox.Show("Veuillez remplir tous les champs.");
            }
            else
            {
                EnvoyerMailFestivaliers(mailSubject, content);
            }
        }

        private void EnvoyerMailFestivaliers(string mailSubject, string content)
        {
            string sendMailTo = "daramorgan69@gmail.com";
            sendMail.ActionSendMail(sendMailTo, mailSubject, content);
            MessageBox.Show("Message envoyé");
        }
    }
}
