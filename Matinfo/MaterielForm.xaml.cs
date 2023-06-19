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
using System.Windows.Shapes;

namespace Matinfo
{
    /// <summary>
    /// Logique d'interaction pour MaterielForm.xaml
    /// </summary>
    public partial class MaterielForm : Window
    {
        public Materiel materiel {  get; set; }

        public MaterielForm(Materiel materiel, bool estFormModification, ApplicationData applicationData)
        {
            InitializeComponent();
            this.DataContext = materiel;
            this.materiel = materiel;
            this.cbCatMateriel.ItemsSource = applicationData.LesCategories;
            cbCatMateriel.DisplayMemberPath = "Nom";
            if (estFormModification)
            {
                this.Title = "Formulaire modification materiel";
                btnConfirmer.Content = "Modifier";
                btnConfirmer.Click -= Button_Click_Ajouter;
                btnConfirmer.Click += Button_Click_Modifier;
            }
        }

        private void Button_Click_Ajouter(object sender, RoutedEventArgs e)
        {
            Materiel materielActuel = new Materiel(materiel.IdMateriel, ((CategorieMateriel)cbCatMateriel.SelectedItem).IdCategorie, tbCodeBarre.Text, tbNomMateriel.Text, tbRefConstructeur.Text);

            cbCatMateriel.GetBindingExpression(ComboBox.SelectedValueProperty).UpdateSource();
            tbNomMateriel.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            tbCodeBarre.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            tbRefConstructeur.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            if (Validation.GetHasError((DependencyObject)tbCodeBarre))
            {
                MessageBox.Show(this.Owner, "Saisie du code barre invalide", "problème code barre", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (materielActuel.Create())
            { 
                DialogResult = true;
            }
            this.materiel.Read();
        }

        private void Button_Click_Modifier(object sender, RoutedEventArgs e)
        {
            Materiel materielActuel = new Materiel(materiel.IdMateriel, ((CategorieMateriel)cbCatMateriel.SelectedItem).IdCategorie, tbCodeBarre.Text, tbNomMateriel.Text, tbRefConstructeur.Text);

            cbCatMateriel.GetBindingExpression(ComboBox.SelectedValueProperty).UpdateSource();
            tbNomMateriel.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            tbCodeBarre.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            tbRefConstructeur.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            if (Validation.GetHasError((DependencyObject)tbCodeBarre))
            {
                MessageBox.Show(this.Owner, "Saisie du code barre invalide", "problème code barre", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (materielActuel.Update())
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
