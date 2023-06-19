using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Matinfo.Metier
{
    /// <summary>
    /// Stocke 2 informations :
    /// 1 chaine : le nom
    /// 1 entier : l'id de la catégorie
    /// </summary>
    public class CategorieMateriel : Crud<CategorieMateriel>
    {
        

        private int idCategorie;
        private string nom;
        private ObservableCollection<Materiel> lesMateriaux;

        public CategorieMateriel()
        {

        }

        public CategorieMateriel(int idCategorie, string nom)
        {
            this.IdCategorie = idCategorie;
            this.Nom = nom;
        }
        /// <summary>
        /// Obtient ou définit le nom de la categorie 
        /// </summary>
        public string Nom
        {
            
            get
            {
                return this.nom;
            }

            set
            {
                this.nom = value;
            }
        }
        /// <summary>
        /// Obtient ou définit l'id de la categorie 
        /// </summary>
        public int IdCategorie
        {
           
            get
            {
                return this.idCategorie;
            }

            set
            {
                this.idCategorie = value;
            }
        }
        /// <summary>
        /// Obtient la base de données des matériaux
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
        /// Creation de la catégorie du matériel
        /// </summary>
        /// <returns>Un vrai si la création à bien marché ou un faux si cela n'a pas marché</returns>
        public bool Create()
        {
       
         
            if (this.Read())
            {
                MessageBox.Show("Erreur lors de la création de la catégorie, il existe déjà une catégorie avec ce nom", "Problème lors de la création", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            ///creation de la catégorie du matériel
            DataAccess accessDB = new DataAccess();
            string requete = string.Format("INSERT INTO categorie_materiel(nomcategorie) VALUES('{0}')", this.Nom);
            accessDB.SetData(requete);
            return true;
        }
        /// <summary>
        /// Suppréssion de la catégorie du matériel
        /// </summary>
        public void Delete()
        {

          

            DataAccess accessDB = new DataAccess();
            string requete = "DELETE FROM categorie_materiel WHERE \"idcategorie\"=" + this.IdCategorie;
            accessDB.SetData(requete);
        }
        /// <summary>
        ///Cherche toutes les catégories de matériaux dans la base de données
        /// </summary>
        /// <returns>Toutes les catégories de matériaux</returns>
        public ObservableCollection<CategorieMateriel> FindAll()
        {
           

            ObservableCollection<CategorieMateriel> LesCategories = new ObservableCollection<CategorieMateriel>();
            DataAccess accesBD = new DataAccess();
            String requete = "select idcategorie, nomcategorie from categorie_materiel ;";
            DataTable datas = accesBD.GetData(requete);
            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    CategorieMateriel c = new CategorieMateriel(int.Parse(row["idcategorie"].ToString()), (String)row["nomcategorie"]);
                    LesCategories.Add(c);
                }
            }
            return LesCategories;
        }

        /// <summary>
        ///Cherche la catégorie de matériel selectionné dans la base de données
        /// </summary>
        /// <returns>Toutes les catégories de matériaux< selectionnées</returns>
        public ObservableCollection<CategorieMateriel> FindBySelection(string criteres)
        {
            ObservableCollection<CategorieMateriel> LesCategories = new ObservableCollection<CategorieMateriel>();
            DataAccess accesBD = new DataAccess();
            String requete = "select idcategorie, nomcategorie, nompersonnel, prenompersonnel from categorie_materiel " + criteres;
            DataTable datas = accesBD.GetData(requete);
            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    CategorieMateriel c = new CategorieMateriel(int.Parse(row["idcategorie"].ToString()), (String)row["nomcategorie"]);
                    LesCategories.Add(c);
                }
            }
            return LesCategories;
        }
        /// <summary>
        ///Cherche si il n'existe pas déjà une catégorie de matériel existante avec le nom de la catégorie
        /// </summary>
        /// <returns>Vrai s'il existe ou faux s'il n'existe pas</returns>
        public bool Read()
        {
            
            int ancienID = this.IdCategorie;
            DataAccess accesBD = new DataAccess();
            String requete = string.Format("select idcategorie from categorie_materiel where nomcategorie = '{0}'", this.Nom);
            DataTable datas = accesBD.GetData(requete);
            if (datas != null)
            {
                if (datas.Rows.Count > 0)
                {
                    this.IdCategorie = int.Parse(datas.Rows[0]["idcategorie"].ToString());
                    if(ancienID != this.IdCategorie)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        ///Modifie une catégorie de matériel existante
        /// </summary>
        /// <returns>Vrai s'il y a bien eu modification ou faux si cela n'a pas été modifié</returns>
        public bool Update()
        {
           
            int idCategorieModifie = this.IdCategorie;
            /// verification que les nouvelles valeurs respectent l'unicité
            if (this.Read())
            {
                MessageBox.Show("Erreur lors de la modification de la categorie, le nouveau nom existe déjà", "Problème lors de la modification", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                this.IdCategorie = idCategorieModifie;
                DataAccess accesBD = new DataAccess();
                String requete = string.Format("update categorie_materiel SET nomcategorie = '{0}' WHERE idcategorie = {1}", this.Nom, this.IdCategorie);
                accesBD.SetData(requete);
            }
            return true;
        }
    }
}
