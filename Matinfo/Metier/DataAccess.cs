using System;
using System.Data;
using System.Windows;
using Npgsql;


namespace Matinfo.Metier
{
    /// <summary>
    /// Gestion de la connexion de la base de donnees
    /// </summary>

    public class DataAccess
   {
       

      public NpgsqlConnection? NpgSQLConnect
      {
         get
         ;
         set
         ;
      }
        /// <summary>
        /// Connexion à la base de donnees
        /// </summary>
        /// <returns> Retourne un booleen indiquant si la connexion est ouverte ou fermee</returns>

        public bool OpenConnection()
      {
           
            try
            {
              NpgSQLConnect = new NpgsqlConnection
              {
                  ConnectionString = "Server=srv-peda-new;port=5433;Database=gehantd;Search Path=sae201;uid=gehantd;password=AWj51O;"
              };
              NpgSQLConnect.Open();
                
                return NpgSQLConnect.State.Equals(System.Data.ConnectionState.Open);
          }
          catch (Exception e)
          {
                MessageBox.Show(e.ToString());
                return false;
          }
      }
        /// <summary>
        /// Deconnexion de la base de donnees
        /// </summary>

        private void CloseConnection()
      {
     
            if (NpgSQLConnect!=null)
              if (NpgSQLConnect.State.Equals(System.Data.ConnectionState.Open))
              {
                  NpgSQLConnect.Close();
              }
      }

        /// <summary>
        /// Accès à des donnees en lecture
        /// </summary>
        /// <returns>Retourne un DataTable contenant les lignes renvoyees par le SELECT</returns>
        /// <param name="getQuery">Requête SELECT de selection de donnees</param>
        public DataTable GetData(string getQuery)
      {

            try
            {
                
                if (OpenConnection())
                {
                    

                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(getQuery, NpgSQLConnect);
                    NpgsqlDataAdapter npgsqlAdapter = new NpgsqlDataAdapter(npgsqlCommand);
                    DataTable dataTable = new DataTable();
                    npgsqlAdapter.Fill(dataTable);
                    CloseConnection();
                    

                    return dataTable;

                }
                else
                {
                   
                    return null;
                }
          }
          catch (Exception e)
          {
              MessageBox.Show(e.ToString());
              CloseConnection();
              return null;
          }
      }

        /// <summary>
        /// Permet d'inserer, supprimer ou modifier des donnees
        /// </summary>
        /// <returns>Retourne un entier contenant le nombre de lignes ajoutees/supprimees/modifiees</returns>
        /// <param name="setQuery">Requête SQL permettant d'inserer (INSERT), supprimer (DELETE) ou modifier des donnees (UPDATE)</param>
        public int SetData(string setQuery)
      {
            
            try
            {
              if (OpenConnection())
              {
                  NpgsqlCommand sqlCommand = new NpgsqlCommand(setQuery, NpgSQLConnect);
                  int modifiedLines = sqlCommand.ExecuteNonQuery();
                  CloseConnection();
                  return modifiedLines;
              }
              else
                  return 0;
          }
          catch 
          {
                
                CloseConnection();
                return 0;
          }
      }
   
   }
}