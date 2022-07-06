using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
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

        private string designation = "";
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
            get { return this.quantiteMini.ToString(); }
            set
            {
                this.quantiteMini = int.Parse(value);
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<string> ListeCategories { get; set; } = new ObservableCollection<string>();

        private string selectedCategorie = "";
        public string SelectedCategorie
        {
            get { return this.selectedCategorie; }
            set
            {
                this.selectedCategorie = value;
                NotifyPropertyChanged();
            }
        }

        private GestionStockClient wsArticleClient = new GestionStockClient();
        private GestionCategorieClient wsCategorieClient = new GestionCategorieClient();

        public AddArticle()
        {
            InitializeComponent();
            DataContext = this;
            var categories = wsCategorieClient.GetCategories();
            foreach (var cat in categories)
            {
                ListeCategories.Add(cat.Nom);
            }
        }

        private void Valider_AjoutArticle(object sender, RoutedEventArgs e)
        {
            var categorie = wsCategorieClient.RechercherCategoriesByMotCle(SelectedCategorie)[0];
            int a = wsArticleClient.AjoutArticle(new Article()
            {
                Categorie = categorie,
                Designation = DesignationTB,
                Prix = double.Parse(PrixTB),
                QteMini = int.Parse(QuantiteMiniTB)
            });
            if (a > 0) MessageBox.Show("L'article a bien été ajouté");
            this.Close();
        }

        private void AnnulerAjouArticle(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

