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

        public Personnel UnPersonnel
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

        public Materiel UnMateriel
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
            ///creation du materiel
            DataAccess accessDB = new DataAccess();
            string requete = string.Format("INSERT INTO attributions(idpersonnel, idmateriel, dateattribution, commentaireattribution) VALUES({0}, {1}, '{2}', '{3}')", +this.IdPersonnel, this.IdMateriel, this.DateAttribution.ToString("dd/MM/yyyy"), this.Commentaire);
            accessDB.SetData(requete);
            return true;
        }

        public void Delete()
        {
            DataAccess accessDB = new DataAccess();
            string requete = "DELETE FROM attributions WHERE (\"idpersonnel\"=" + this.IdPersonnel + " AND \"idmateriel\"=" + this.IdMateriel + " AND \"dateattribution\"=" + this.DateAttribution + ")";
            accessDB.SetData(requete);
        }

        public ObservableCollection<Attribution> FindAll()
        {
            ObservableCollection<Attribution> LesAttributions = new ObservableCollection<Attribution>();
            DataAccess accesBD = new DataAccess();
            String requete = "select idpersonnel, idmateriel, dateattribution, commentaireattribution from attributions";
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
            ObservableCollection<Attribution> LesAttributions = new ObservableCollection<Attribution>();
            DataAccess accesBD = new DataAccess();
            String requete = "select idpersonnel, idmateriel, dateattribution, commentaireattribution from attributions " + criteres;
            DataTable datas = accesBD.GetData(requete);
            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    Attribution a = new Attribution(int.Parse(row["idpersonnel"].ToString()), int.Parse(row["idmateriel"].ToString()), (String)row["commentaireattribution"], (DateTime)row["commentaireattribution"]);
                    LesAttributions.Add(a);
                }
            }
            return LesAttributions;
        }

        public bool Read()
        {
            DataAccess accesBD = new DataAccess();
            String requete = string.Format("select * from attributions WHERE (idpersonnel = {0} AND idmateriel = {1} AND dateattribution = '{2}')", this.idPersonnel, this.IdMateriel, this.DateAttribution.ToString("dd/MM/yyyy"));
            DataTable datas = accesBD.GetData(requete);
            if (datas != null)
            {
                if (datas.Rows.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Update()
        {
            DataAccess accesBD = new DataAccess();
            String requete = string.Format("update attributions SET dateattribution = '{0}', commentaireattribution = '{1}' WHERE (idpersonnel= {2} AND idmateriel= {3} AND dateattribution= {4}", this.DateAttribution.ToString("dd/MM/yyyy"), this.Commentaire, this.IdPersonnel, this.IdMateriel, this.DateAttribution);
            accesBD.SetData(requete);
            return true;
        }
    }
}
