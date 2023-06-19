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
    /// 1 entier : l'id de la categorie
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
        /// Obtient ou definit le nom de la categorie 
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
        /// Obtient ou definit l'id de la categorie 
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
        /// Obtient la base de donnees des materiaux
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
        ///Creation de la categorie du materiel
        /// </summary>
        /// <returns>Un vrai si la creation à bien marche ou un faux si cela n'a pas marche</returns>
        public bool Create()
        {
       
         
            if (this.Read())
            {
                MessageBox.Show("Erreur lors de la creation de la categorie, il existe dejà une categorie avec ce nom", "Problème lors de la creation", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            ///creation de la categorie du materiel
            DataAccess accessDB = new DataAccess();
            string requete = string.Format("INSERT INTO categorie_materiel(nomcategorie) VALUES('{0}')", this.Nom);
            accessDB.SetData(requete);
            return true;
        }
        /// <summary>
        ///Suppression de la categorie du materiel
        /// </summary>
        public void Delete()
        {

          

            DataAccess accessDB = new DataAccess();
            string requete = "DELETE FROM categorie_materiel WHERE \"idcategorie\"=" + this.IdCategorie;
            accessDB.SetData(requete);
        }
        /// <summary>
        ///Cherche toutes les categories de materiaux dans la base de donnees
        /// </summary>
        /// <returns>Toutes les categories de materiaux</returns>
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
        ///Cherche la categorie de materiel selectionne dans la base de donnees
        /// </summary>
        ///<returns>Toutes les categories de materiaux< selectionnees</returns>
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
        ///Cherche si il n'existe pas dejà une categorie de materiel existante avec le nom de la categorie
        /// </summary>
        ///<returns>Vrai s'il existe ou faux s'il n'existe pas</returns>
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
        ///Modifie une categorie de materiel existante
        /// </summary>
        ///<returns>Vrai s'il y a bien eu modification ou faux si cela n'a pas ete modifie</returns>
        public bool Update()
        {
           
            int idCategorieModifie = this.IdCategorie;
            /// verification que les nouvelles valeurs respectent l'unicite
            if (this.Read())
            {
                MessageBox.Show("Erreur lors de la modification de la categorie, le nouveau nom existe dejà", "Problème lors de la modification", MessageBoxButton.OK, MessageBoxImage.Error);
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
