using Matinfo.Metier;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Matinfo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            ApplicationData data = new ApplicationData();
        }

     

        private void btAjoutPersonnel_Click(object sender, RoutedEventArgs e)
        {
            Personnel persoCree = new Personnel();
            PersonnelForm formperso = new PersonnelForm(persoCree, false);
            formperso.Owner = this;
            bool result = (bool)formperso.ShowDialog();
            if (result)
            {
                applicationData.LesPersonnels.Add(persoCree);
                applicationData.RefreshAssociationsPersonnel();
            }
        }

        private void btModifPersonnel_Click(object sender, RoutedEventArgs e)
        {
            PersonnelForm formpersonnel = new PersonnelForm((Personnel)lvPersonnel.SelectedItem, true);
            formpersonnel.Owner = this;
            bool result = (bool)formpersonnel.ShowDialog();
            if (result)
            {
                lvPersonnel.Items.Refresh();
                applicationData.RefreshAssociationsPersonnel();
            }
        }

        private void Button_Click_RemovePersonnel(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultat = MessageBoxResult.Yes;
            int nbAssociationsLies = ((Personnel)this.lvPersonnel.SelectedItem).LesAttributions.Count;
            /// si le personnel a des associations on demande confirmation pour la supression
            if (nbAssociationsLies > 0)
            {
                resultat = MessageBox.Show("Etes vous sûr de vouloir supprimer ce personnel ? Cela supprimera aussi ses " + nbAssociationsLies + " Attributions !", "Confirmer la suppression?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            }

            if (resultat == MessageBoxResult.Yes)
            {
                ((Personnel)lvPersonnel.SelectedItem).Delete();
                foreach (Attribution attribution in ((Personnel)lvPersonnel.SelectedItem).LesAttributions)
                {
                    attribution.Delete();
                    applicationData.LesAttributions.Remove(attribution);
                }
                applicationData.LesPersonnels.Remove((Personnel)lvPersonnel.SelectedItem);
            }
        }

        private void Button_Click_FormMateriel_Ajout(object sender, RoutedEventArgs e)
        {
            Materiel materielCree = new Materiel();
            MaterielForm formMateriel = new MaterielForm(materielCree, false, this.applicationData);
            formMateriel.Owner = this;
            bool result = (bool)formMateriel.ShowDialog();
            if (result)
            {
                applicationData.LesMateriaux.Add(materielCree);
                applicationData.RefreshAssociationsMateriaux();
            }
        }

        private void Button_Click_FormMateriel_Modif(object sender, RoutedEventArgs e)
        {
            MaterielForm formMateriel = new MaterielForm((Materiel)lvMateriel.SelectedItem, true, this.applicationData);
            formMateriel.Owner = this;
            bool result = (bool)formMateriel.ShowDialog();
            if (result)
            {
                lvMateriel.Items.Refresh();
                applicationData.RefreshAssociationsMateriaux();
            }
        }

        private void Button_Click_RemoveMateriel(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultat = MessageBoxResult.Yes;
            int nbAssociationsLies = ((Materiel)this.lvMateriel.SelectedItem).LesAttributions.Count;
            /// si le materiel a des associations on demande confirmation pour la supression
            if(nbAssociationsLies > 0)
            {
                resultat = MessageBox.Show("Etes vous sûr de vouloir supprimer ce materiel ? Cela supprimera aussi ses " + nbAssociationsLies + " Attributions !", "Confirmer la suppression?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            }

            if (resultat == MessageBoxResult.Yes)
            {
                ((Materiel)lvMateriel.SelectedItem).Delete();
                foreach(Attribution attribution in ((Materiel)lvMateriel.SelectedItem).LesAttributions)
                {
                    attribution.Delete();
                    applicationData.LesAttributions.Remove(attribution);
                }
                applicationData.LesMateriaux.Remove((Materiel)lvMateriel.SelectedItem);
            }
            applicationData.RefreshAssociationsMateriaux();
        }

        private void Button_Click_AjoutFormCategorie(object sender, RoutedEventArgs e)
        {
            CategorieMateriel categorieCree = new CategorieMateriel();
            CategorieForm formCategorie = new CategorieForm(categorieCree, false);
            formCategorie.Owner = this;
            bool result = (bool)formCategorie.ShowDialog();
            if (result)
            {
                applicationData.LesCategories.Add(categorieCree);
                applicationData.RefreshAssociationsMateriaux();
            }
        }

        private void Button_Click_ModifFormCategorie(object sender, RoutedEventArgs e)
        {
            CategorieForm formCategorie = new CategorieForm((CategorieMateriel)lvCategorie.SelectedItem, true);
            formCategorie.Owner = this;
            bool result = (bool)formCategorie.ShowDialog();
            if (result)
            {
                lvCategorie.Items.Refresh();
                applicationData.RefreshAssociationsMateriaux();
            }
        }

        private void Button_Click_RemoveCategorie(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultat = MessageBoxResult.Yes;
            int nbMateriauxLies = ((CategorieMateriel)this.lvCategorie.SelectedItem).LesMateriaux.Count;
            /// si la categorie a des materiaux alors on demande confirmation pour la suppression
            if (nbMateriauxLies > 0)
            {
                resultat = MessageBox.Show("Etes vous sûr de vouloir supprimer ce materiel ? Cela supprimera aussi ses " + nbMateriauxLies + " materiaux ainsi que les attributions qui y sont liées !", "Confirmer la suppression?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            }

            if (resultat == MessageBoxResult.Yes)
            {
                ((CategorieMateriel)lvCategorie.SelectedItem).Delete();
                foreach (Materiel materiel in ((CategorieMateriel)lvCategorie.SelectedItem).LesMateriaux)
                {
                    foreach(Attribution attribution in materiel.LesAttributions)
                    {
                        attribution.Delete();
                        applicationData.LesAttributions.Remove(attribution);
                    }
                    materiel.Delete();
                    applicationData.LesMateriaux.Remove(materiel);
                }
                applicationData.LesCategories.Remove((CategorieMateriel)lvCategorie.SelectedItem);
            }
        }

        private void Button_Click_FormAjoutAttribution(object sender, RoutedEventArgs e)
        {
            Attribution attributionCree = new Attribution();
            AttributionFormAjout formAjoutAttribution = new AttributionFormAjout(attributionCree, this.applicationData);
            formAjoutAttribution.Owner = this;
            bool result = (bool)formAjoutAttribution.ShowDialog();
            if (result)
            {
                applicationData.LesAttributions.Add(attributionCree);
                applicationData.RefreshAssociationsMateriaux();
                applicationData.RefreshAssociationsPersonnel();
            }
        }

        private void Button_Click_FormModifAttribution(object sender, RoutedEventArgs e)
        {
            AttributionFormModif formModifAttribution = new AttributionFormModif((Attribution)lvAttribution.SelectedItem);
            formModifAttribution.Owner = this;
            bool result = (bool)formModifAttribution.ShowDialog();
            if (result)
            {
                lvAttribution.Items.Refresh();
                applicationData.RefreshAssociationsMateriaux();
                applicationData.RefreshAssociationsPersonnel();
            }
        }

        private void Button_Click_RemoveAttribution(object sender, RoutedEventArgs e)
        {
            ((Attribution)lvAttribution.SelectedItem).Delete();
            applicationData.LesAttributions.Remove((Attribution)lvAttribution.SelectedItem);
        }

        private void lvMateriel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lvMateriel.SelectedItem != null)
            {
                lvAttribution.ItemsSource = ((Materiel)lvMateriel.SelectedItem).LesAttributions;
                btnRefreshAttribution.Visibility = Visibility.Visible;
            }
            else
            {
                btnRefreshAttribution.Visibility = Visibility.Hidden;
                lvAttribution.ItemsSource = applicationData.LesAttributions;
            }
        }

        private void btnRefreshAttribution_Click(object sender, RoutedEventArgs e)
        {
            lvMateriel.SelectedItem = null;
        }

        private void lvCategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvCategorie.SelectedItem != null)
            {
                btnDeselecCat.Visibility = Visibility.Visible;
                lvMateriel.SelectedItem = null;
                lvMateriel.ItemsSource = ((CategorieMateriel)lvCategorie.SelectedItem).LesMateriaux;
            }
            else
            {
                btnDeselecCat.Visibility = Visibility.Hidden;
                lvMateriel.ItemsSource = "";
            }
        }

        private void btnDeselecCat_Click(object sender, RoutedEventArgs e)
        {
            lvCategorie.SelectedItem = null;
        }
    }
}
