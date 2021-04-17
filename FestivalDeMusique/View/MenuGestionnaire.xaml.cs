using System;
using System.Collections.Generic;
using System.Text;
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
    /// Logique d'interaction pour MenuGestionnaire.xaml
    /// </summary>
    public partial class MenuGestionnaire : Window
    {
        public MenuGestionnaire()
        {
            InitializeComponent();
        }

        private void Open_Gestion_Festival(object sender, RoutedEventArgs e)
        {
            Hide();
            GestionFestival gestionFestival = new GestionFestival();
            gestionFestival.ShowDialog();
            _ = ShowDialog();
        }

        private void Open_Statistiques(object sender, RoutedEventArgs e)
        {

        }
    }
}
