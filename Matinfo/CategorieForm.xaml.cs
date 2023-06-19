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
            /// si le matériel a bien été créé dans la base, on l'ajoute dans l'application
            if (categorieActuelle.Create())
            {
                tbNomCat.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                DialogResult = true;
            }
        }

        private void Button_Click_Modifier(object sender, RoutedEventArgs e)
        {
            CategorieMateriel categorieActuelle = new CategorieMateriel(categorie.IdCategorie, tbNomCat.Text);
            /// si le matériel a bien été créé dans la base, on l'ajoute dans l'application
            if (categorieActuelle.Update())
            {
                tbNomCat.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                DialogResult = true;
            }
        }

        private void Button_Click_Annuler(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
