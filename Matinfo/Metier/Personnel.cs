using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matinfo.Metier
{
    public class Personnel : Crud<Personnel>
    {
        private int idPersonnel;
        private string email;
        private string nom;
        private string prenom;

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

        public bool Create()
        {
            return false;
        }

        public void Delete()
        {
            throw new NotImplementedException();
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
