using Matinfo.Metier;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Matinfo
{
    /// <summary>
    /// Logique d'interaction pour CategorieForm.xaml
    /// </summary>
    public partial class CategorieForm : Window
    {
        public CategorieMateriel categorie { get; set; }

        public CategorieForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ajoute une catégorie de matériel avec le formulaire
        /// </summary>
        public CategorieForm(CategorieMateriel categorie, bool estFormModification)
        {
            InitializeComponent();
            this.DataContext = categorie;
            this.categorie = categorie;
            if (estFormModification)
            {
                this.Title = "Formulaire modification categorie matériel";
                btnConfirmation.Content = "Modifier";
                btnConfirmation.Click -= Button_Click_Ajouter;
                btnConfirmation.Click += Button_Click_Modifier;
            }
        }

        private void Button_Click_Ajouter(object sender, RoutedEventArgs e)
        {
            CategorieMateriel categorieActuelle = new CategorieMateriel(categorie.IdCategorie, tbNomCat.Text);
            tbNomCat.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            if (Validation.GetHasError((DependencyObject)tbNomCat))
            {
                MessageBox.Show(this.Owner, "Saisie du nom de catégorie invalide", "problème nom catégorie", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (categorieActuelle.Create())
            {
                DialogResult = true;
            }
            this.categorie.Read();
        }

        private void Button_Click_Modifier(object sender, RoutedEventArgs e)
        {
            CategorieMateriel categorieActuelle = new CategorieMateriel(categorie.IdCategorie, tbNomCat.Text);
            tbNomCat.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            if (Validation.GetHasError((DependencyObject)tbNomCat))
            {
                MessageBox.Show(this.Owner, "Saisie du nom de catégorie invalide", "problème nom catégorie", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (categorieActuelle.Update())
            {
                DialogResult = true;
            }
        }

        private void Button_Click_Annuler(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
