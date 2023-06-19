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
    /// Logique d'interaction pour AttributionFormAjout.xaml
    /// </summary>
    public partial class AttributionFormAjout : Window
    {
        public Attribution attribution { get; set; }


        /// <summary>
        /// Ajoute une attribution avec le formulaire
        /// </summary>
        public AttributionFormAjout(Attribution attribution, ApplicationData applicationData)
        {

            InitializeComponent();
            this.attribution = attribution;
            this.DataContext = attribution;
            this.attribution.DateAttribution = DateTime.Today;
            this.cbMateriel.ItemsSource = applicationData.LesMateriaux;
            this.cbPersonnel.ItemsSource = applicationData.LesPersonnels;
            cbMateriel.DisplayMemberPath = "Nom";
            cbPersonnel.DisplayMemberPath = "Nom";
        }
        /// <summary>
        /// Teste la vérification et la valide ou non
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <re
        private void Button_Click_Ajouter(object sender, RoutedEventArgs e)
        {
            Attribution attributionActuelle = new Attribution(((Personnel)cbPersonnel.SelectedItem).IdPersonnel, ((Materiel)cbMateriel.SelectedItem).IdMateriel, tbCommentaire.Text, (DateTime)dpDate.SelectedDate);
            /// test si il existe déjà une même attribution
            if (attributionActuelle.Read())
            {
                MessageBox.Show("Erreur lors de la création de l'attribution : il existe déjà une attribution entre le materiel et le personnel à la date donnée", "Problème lors de la modification", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            attributionActuelle.Create();
            this.attribution = attributionActuelle;
            cbMateriel.GetBindingExpression(ComboBox.SelectedValueProperty).UpdateSource();
            cbPersonnel.GetBindingExpression(ComboBox.SelectedValueProperty).UpdateSource();
            tbCommentaire.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            dpDate.GetBindingExpression(DatePicker.SelectedDateProperty).UpdateSource();
            DialogResult = true;
        }

        private void Button_Click_Annuler(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
