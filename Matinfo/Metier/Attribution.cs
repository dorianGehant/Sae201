using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matinfo.Metier
{
    public class Attribution : Crud<Attribution>
    {
        private int idPersonnel;
        private int idMateriel;
        private string commentaire;
        private DateTime dateAttribution;
        private Personnel unPersonnel;
        private Materiel unMateriel;

        public Attribution()
        {

        }

        public Attribution(int idPersonnel, int idMateriel, string commentaire, DateTime dateAttribution)
        {
            this.Commentaire = commentaire;
            this.DateAttribution = dateAttribution;
            this.idPersonnel = idPersonnel;
            this.idMateriel = idMateriel;
        }

        public string Commentaire
        {
            get
            {
                return this.commentaire;
            }

            set
            {
                this.commentaire = value;
            }
        }

        public DateTime DateAttribution
        {
            get
            {
                return this.dateAttribution;
            }

            set
            {
                this.dateAttribution = value;
            }
        }

        public int IdPersonnel
        {
            get
            {
                return this.idPersonnel;
            }

            set
            {
                this.idPersonnel = value;
            }
        }

        public int IdMateriel
        {
            get
            {
                return this.idMateriel;
            }

            set
            {
                this.idMateriel = value;
            }
        }

        internal Personnel UnPersonnel
        {
            get
            {
                return this.unPersonnel;
            }

            set
            {
                this.unPersonnel = value;
            }
        }

        internal Materiel UnMateriel
        {
            get
            {
                return this.unMateriel;
            }

            set
            {
                this.unMateriel = value;
            }
        }

        public bool Create()
        {
            return false;
        }

        public void Delete()
        {
            
        }

        public ObservableCollection<Attribution> FindAll()
        {
            ObservableCollection<Attribution> LesAttributions = new ObservableCollection<Attribution>();
            DataAccess accesBD = new DataAccess();
            String requete = "select idpersonnel, idmateriel, dateattribution, commentaireattribution from attributions ;";
            DataTable datas = accesBD.GetData(requete);
            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    Attribution a = new Attribution(int.Parse(row["idpersonnel"].ToString()), int.Parse(row["idmateriel"].ToString()), (String)row["commentaireattribution"], DateTime.Parse(row["dateattribution"].ToString()));
                    LesAttributions.Add(a);
                }
            }
            return LesAttributions;
        }

        public ObservableCollection<Attribution> FindBySelection(string criteres)
        {
            return new ObservableCollection<Attribution>();
        }

        public void Read()
        {
            
        }

        public bool Update()
        {
            return false;
        }
    }
}
