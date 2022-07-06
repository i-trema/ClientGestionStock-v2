using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using WSGestionStock;

namespace ClientGestionStock
{
    /// <summary>
    /// Logique d'interaction pour AddArticle.xaml
    /// </summary>
    public partial class AddArticle : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
        private double? prix = 1.0;
        public string PrixTB
        {
            get { return this.prix.ToString(); }
            set
            {
                this.prix = double.Parse(value);
                NotifyPropertyChanged();
            }
        }

        private string designation;
        public string DesignationTB
        {
            get { return this.designation; }
            set
            {
                this.designation = value;
                NotifyPropertyChanged();
            }
        }

        private int? quantiteMini = 1;
        public string QuantiteMiniTB
        {
            get { return this.prix.ToString(); }
            set
            {
                this.prix = int.Parse(value);
                NotifyPropertyChanged();
            }
        }





        public ObservableCollection<string> ListeCategories { get; set; } = new ObservableCollection<string>();

        private string selectedCategorie;
        public string SelectedCategorie
        {
            get { return this.selectedCategorie; }
            set
            {
                this.selectedCategorie = value;
                NotifyPropertyChanged();
            }
        }

        public AddArticle()
        {
            InitializeComponent();
            DataContext = this;
            GestionCategorieClient wsCategorieClient = new GestionCategorieClient();
            var categories = wsCategorieClient.GetCategories();
            foreach (var cat in categories)
            {
                ListeCategories.Add(cat.Nom);
            }
        }

        private void Valider_AjoutArticle(object sender, RoutedEventArgs e)
        {
            GestionStockClient wsArticleClient = new GestionStockClient();
            GestionCategorieClient wsCategorieClient = new GestionCategorieClient();

            var categorie = wsCategorieClient.RechercherCategoriesByMotCle(SelectedCategorie)[0];
            wsArticleClient.AjoutArticle(new Article()
            {
                Categorie = categorie,
                Designation = DesignationTB,
                Prix = double.Parse(PrixTB),
                QteMini = int.Parse(QuantiteMiniTB)
            });

            this.Close();

            
            //AddArticle addArticle = new AddArticle();
            //addArticle.Owner = this;
            //addArticle.ShowDialog();


        }

        private void AnnulerAjouArticle(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

