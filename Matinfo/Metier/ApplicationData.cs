using Matinfo.Metier;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Matinfo.Metier
{
    /// <summary>
    /// Stocke 4 informations :
    /// 4 ObservableCollection : les materiaux, les personnels, les categories, les attributions
    /// </summary>
    public class ApplicationData
    {
        
        private ObservableCollection<Materiel> lesMateriaux;
        private ObservableCollection<Personnel> lesPersonnels;
        private ObservableCollection<CategorieMateriel> lesCategories;
        private ObservableCollection<Attribution> lesAttributions;

        public ApplicationData()
        {
            Materiel m = new Materiel();
            LesMateriaux = m.FindAll();

            Personnel p = new Personnel();
            LesPersonnels = p.FindAll();

            CategorieMateriel c = new CategorieMateriel();
            LesCategories = c.FindAll();

            Attribution a = new Attribution();
            LesAttributions = a.FindAll();

            /// Mise en place des associations ///
            RefreshAssociationsMateriaux();
            RefreshAssociationsPersonnel();
        }
        /// <summary>
        /// Obtient ou definit les materiaux
        /// </summary>
        public ObservableCollection<Materiel> LesMateriaux
        {
           
            get
            {
                return this.lesMateriaux;
            }

            set
            {
                this.lesMateriaux = value;
            }
        }
        /// <summary>
        /// Obtient ou definit les personnels
        /// </summary>
        public ObservableCollection<Personnel> LesPersonnels
        {
           
            get
            {
                return this.lesPersonnels;
            }

            set
            {
                this.lesPersonnels = value;
            }
        }
        /// <summary>
        /// Obtient ou definit les categories
        /// </summary>
        public ObservableCollection<CategorieMateriel> LesCategories
        {
           
            get
            {
                return this.lesCategories;
            }

            set
            {
                this.lesCategories = value;
            }
        }
        /// <summary>
        /// Obtient ou definit les attributions
        /// </summary>
        public ObservableCollection<Attribution> LesAttributions
        {
            
            get
            {
                return this.lesAttributions;
            }

            set
            {
                this.lesAttributions = value;
            }
        }

        /// <summary>
        /// Rafraichit les materiaux et ses associations
        /// </summary>
        public void RefreshAssociationsMateriaux()
        {
            

            /// Liste des attributions pour chaque materiel ///
            foreach (Materiel unMateriel in LesMateriaux.ToList())
            {
                unMateriel.LesAttributions = new ObservableCollection<Attribution>(
                LesAttributions.ToList().FindAll(e => e.IdMateriel == unMateriel.IdMateriel)); 
            }
            
            /// Categorie de chaque materiel ///
            foreach (Materiel unMateriel in lesMateriaux.ToList())
            {
                unMateriel.UneCategorie = LesCategories.ToList().Find(g => g.IdCategorie == unMateriel.IdCategorie);
            }

            /// materiel de chaque attribution ///
            foreach(Attribution uneAttribution in LesAttributions.ToList())
            {
                uneAttribution.UnMateriel = LesMateriaux.ToList().Find(g => g.IdMateriel == uneAttribution.IdMateriel);
            }


            /// liste des materiaux pour chaque categorie ///
            foreach (CategorieMateriel categorie in LesCategories.ToList())
            {
                categorie.LesMateriaux = new ObservableCollection<Materiel>(
                LesMateriaux.ToList().FindAll(e => e.IdCategorie == categorie.IdCategorie));
            }
        }
        /// <summary>
        /// Rafraichit les personnels et ses associations
        /// </summary>
        public void RefreshAssociationsPersonnel()
        {
            
            
            /// Liste des attributions pour chaque personnels ///
            foreach (Personnel unPerso in LesPersonnels.ToList())
            {
                unPerso.LesAttributions = new ObservableCollection<Attribution>(
                LesAttributions.ToList().FindAll(e => e.IdPersonnel == unPerso.IdPersonnel));
            }

            /// personnel de chaque attribution ///
            foreach (Attribution uneAttribution in LesAttributions.ToList())
            {
                uneAttribution.UnPersonnel = LesPersonnels.ToList().Find(g => g.IdPersonnel == uneAttribution.IdPersonnel);
            }
        }
    }
}
