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
    /// Logique d'interaction pour PageCompte.xaml
    /// </summary>
    public partial class PageCompte : Page
    {
        private readonly Gimi gimi;
        public PageCompte()
        {
            InitializeComponent();
        }

        public PageCompte(Gimi gimi)
        {
            InitializeComponent();
            this.gimi = gimi;
        }
    }
}
