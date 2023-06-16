using Matinfo.Metier;
using Microsoft.VisualBasic;
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
    /// Logique d'interaction pour AttributionFormModif.xaml
    /// </summary>
    public partial class AttributionFormModif : Window
    {
        public Attribution attribution { get; set; }

        public AttributionFormModif(Attribution attribution)
        {
            InitializeComponent();
            this.attribution = attribution;
            this.DataContext = attribution;
        }

        private void Button_Click_Ajouter(object sender, RoutedEventArgs e)
        {
            Attribution attributionActuelle = new Attribution(attribution.IdMateriel, attribution.IdMateriel, tbCommentaire.Text, (DateTime)dpDate.SelectedDate);
            
            if(attributionActuelle.DateAttribution != this.attribution.DateAttribution)
            {
                /// test si il existe déjà une même attribution
                if (attributionActuelle.Read())
                {
                    MessageBox.Show("Erreur lors de la modification de l'attribution : il existe déjà une attribution entre le materiel et le personnel à la date donnée", "Problème lors de la modification", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            attributionActuelle.Update();
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
