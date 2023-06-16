using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matinfo.Metier
{
    public class CategorieMateriel : Crud<CategorieMateriel>
    {
        private int idCategorie;
        private string nom;

        public CategorieMateriel()
        {

        }

        public CategorieMateriel(int idCategorie, string nom)
        {
            this.IdCategorie = idCategorie;
            this.Nom = nom;
        }

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

        public bool Create()
        {
            return false;
        }

        public void Delete()
        {

        }

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

        public ObservableCollection<CategorieMateriel> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public bool Update()
        {
            return false;
        }
    }
}
