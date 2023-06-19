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
    public class Personnel : Crud<Personnel>
    {
        private int idPersonnel;
        private string email;
        private string nom;
        private string prenom;
        private ObservableCollection<Attribution> lesAttributions;


        public Personnel()
        {

        }

        public Personnel(int idPersonnel, string email, string prenom, string nom)
        {
            this.IdPersonnel = idPersonnel;
            this.Email = email;
            this.Nom = nom;
            this.Prenom = prenom;
        }

        public string Email
        {
            get
            {
                return this.email;
            }

            set
            {
                this.email = value;
            }
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

        public string Prenom
        {
            get
            {
                return this.prenom;
            }

            set
            {
                this.prenom = value;
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

        public bool Create()
        {
            ///verification que le personnel n'existe pas déjà
            if (this.Read())
            {
                MessageBox.Show("Erreur lors de la création du personnel, l'email existe déjà, veuillez le changer", "Problème lors de la création", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            ///creation du personnel
            DataAccess accessDB = new DataAccess();
            string requete = string.Format("INSERT INTO personnel(nompersonnel, prenompersonnel, emailpersonnel) VALUES('{0}', '{1}', '{2}')", this.Nom, this.Prenom, this.Email);
            accessDB.SetData(requete);
            return true;
        }

        public void Delete()
        {
            DataAccess accessDB = new DataAccess();
            string requete = string.Format("DELETE FROM personnel WHERE idpersonnel = {0}", this.IdPersonnel);
            accessDB.SetData(requete);
        }

        public ObservableCollection<Personnel> FindAll()
        {
            ObservableCollection<Personnel> LesPersonnels = new ObservableCollection<Personnel>();
            DataAccess accesBD = new DataAccess();
            String requete = "select idpersonnel, emailpersonnel, nompersonnel, prenompersonnel from personnel ;";
            DataTable datas = accesBD.GetData(requete);
            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    Personnel p = new Personnel(int.Parse(row["idpersonnel"].ToString()), (String)row["emailpersonnel"], (String)row["nompersonnel"], (String)row["prenompersonnel"]);
                    LesPersonnels.Add(p);
                }
            }
            return LesPersonnels;
        }

        public ObservableCollection<Personnel> FindBySelection(string criteres)
        {
            ObservableCollection<Personnel> LesPersonnels = new ObservableCollection<Personnel>();
            DataAccess accesBD = new DataAccess();
            String requete = "select idpersonnel, emailpersonnel, nompersonnel, prenompersonnel from personnel " + criteres;
            DataTable datas = accesBD.GetData(requete);
            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    Personnel m = new Personnel(int.Parse(row["idpersonnel"].ToString()), (String)row["emailpersonnel"], (String)row["prenompersonnel"], (String)row["nompersonnel"]);
                    LesPersonnels.Add(m);
                }
            }
            return LesPersonnels;
        }

        public bool Read()
        {
            int ancienID = this.IdPersonnel;
            DataAccess accesBD = new DataAccess();
            String requete = string.Format("select idpersonnel from personnel where emailpersonnel = '{0}'", this.Email);
            DataTable datas = accesBD.GetData(requete);
            if (datas != null)
            {
                if (datas.Rows.Count > 0)
                {
                    this.IdPersonnel = int.Parse(datas.Rows[0]["idpersonnel"].ToString());
                    if(ancienID != this.IdPersonnel)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool Update()
        {
            int idPersonnelModifie = this.IdPersonnel;
            /// verification que les nouvelles valeurs respectent l'unicité
            if(this.Read())
            {
                MessageBox.Show("Erreur lors de la modification du personnel, le nouvel email existe déjà", "Problème lors de la modification", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                this.IdPersonnel = idPersonnelModifie;
                DataAccess accesBD = new DataAccess();
                String requete = string.Format("update personnel SET nompersonnel = '{0}', prenompersonnel = '{1}', emailpersonnel = '{2}' WHERE idpersonnel = {3}", this.Nom, this.Prenom, this.Email, this.IdPersonnel);
                accesBD.SetData(requete);
            }
            return true;
        }
    }
}
