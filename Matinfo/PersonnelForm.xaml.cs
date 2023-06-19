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
    /// Logique d'interaction pour PersonnelForm.xaml
    /// </summary>
    public partial class PersonnelForm : Window
    {
        public Personnel personnel { get; set; }
        public PersonnelForm(Personnel personnel, bool estFormModification)
        {
            InitializeComponent();
            this.DataContext = personnel;
            this.personnel = personnel;
            if (estFormModification)
            {
                this.Title = "Formulaire modification personnel";
                btnConfirmer.Content = "Modifier";
                btnConfirmer.Click -= Button_Click_Ajouter;
                btnConfirmer.Click += Button_Click_Modifier;
                
            }
        }

        private void Button_Click_Ajouter(object sender, RoutedEventArgs e)
        {
            Personnel personnelActuel = new Personnel(personnel.IdPersonnel, tbEmail.Text, tbPrenom.Text, tbNom.Text);
            tbNom.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            tbPrenom.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            tbEmail.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            if (Validation.GetHasError((DependencyObject)tbEmail))
            {
                MessageBox.Show(this.Owner, "Saisie de l'email invalide", "problème email", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (personnelActuel.Create())
            {
                DialogResult = true;
            }
            this.personnel.Read();
        }

        private void Button_Click_Modifier(object sender, RoutedEventArgs e)
        {
            Personnel personnelActuel = new Personnel(personnel.IdPersonnel, tbEmail.Text, tbPrenom.Text, tbNom.Text);
            /// si le personnel a bien été mis à jour, on le met à jour dans l'application

            tbNom.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            tbPrenom.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            tbEmail.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            if (Validation.GetHasError((DependencyObject)tbEmail))
            {
                MessageBox.Show(this.Owner, "Saisie de l'email invalide", "problème email", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (personnelActuel.Update())
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
