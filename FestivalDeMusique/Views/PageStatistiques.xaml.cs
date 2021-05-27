using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Logique d'interaction pour PageStatistiques.xaml
    /// </summary>
    public partial class PageStatistiques : Page
    {
        public PageStatistiques()
        {
            InitializeComponent();
        }

        private void RapportTemps_ButtonClick(object sender, RoutedEventArgs e)
        {
            //string target = "https://localhost:44344/Rapport_temps";
            string target = "https://localhost:44344/Rapport_Activites/Rapport_Activite_Festival_Graphe/";
            OpenWebPage(target);
        }

        private void RapportGeo_ButtonClick(object sender, RoutedEventArgs e)
        {
            string target = "https://localhost:44344/Rapport_Geos/IndexPays";
            OpenWebPage(target);
        }

        private void RapportActivites_ButtonClick(object sender, RoutedEventArgs e)
        {
            string target = "https://localhost:44344/Rapport_Activites";
            OpenWebPage(target);
        }

        private void OpenWebPage(string target)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo()
                {
                    FileName = target,
                    UseShellExecute = true
                };
                System.Diagnostics.Process.Start(startInfo);
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }
    }
}
