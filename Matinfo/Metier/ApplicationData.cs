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
        }

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


        public void RefreshAssociationsMateriaux()
        {
            /// Liste des attributions pour chaque materiel ///
            foreach (Materiel unMateriel in LesMateriaux.ToList())
            {
                unMateriel.LesAttributions = new ObservableCollection<Attribution>(
                LesAttributions.ToList().FindAll(e => e.IdMateriel == unMateriel.IdMateriel));
            }

            /// Categorie de chaque matériel ///
            foreach (Materiel unMateriel in lesMateriaux.ToList())
            {
                unMateriel.UneCategorie = LesCategories.ToList().Find(g => g.IdCategorie == unMateriel.IdCategorie);
            }
        }
    }
}
