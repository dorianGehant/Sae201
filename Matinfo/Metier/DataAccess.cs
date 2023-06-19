using System;
using System.Data;
using System.Windows;
using Npgsql;


namespace Matinfo.Metier
{

    public class DataAccess
   {

      public NpgsqlConnection? NpgSQLConnect
      {
         get
         ;
         set
         ;
      }
      
      
      public bool OpenConnection()
      {
            /// <summary>
            /// Connexion à la base de données
            /// </summary>
            /// <returns> Retourne un booléen indiquant si la connexion est ouverte ou fermée</returns>
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
      
      
      private void CloseConnection()
      {
            /// <summary>
            /// Déconnexion de la base de données
            /// </summary>
            if (NpgSQLConnect!=null)
              if (NpgSQLConnect.State.Equals(System.Data.ConnectionState.Open))
              {
                  NpgSQLConnect.Close();
              }
      }
      
      public DataTable GetData(string getQuery)
      {

            /// <summary>
            /// Accès à des données en lecture
            /// </summary>
            /// <returns>Retourne un DataTable contenant les lignes renvoyées par le SELECT</returns>
            /// <param name="getQuery">Requête SELECT de sélection de données</param>
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
      

      public int SetData(string setQuery)
      {
            /// <summary>
            /// Permet d'insérer, supprimer ou modifier des données
            /// </summary>
            /// <returns>Retourne un entier contenant le nombre de lignes ajoutées/supprimées/modifiées</returns>
            /// <param name="setQuery">Requête SQL permettant d'insérer (INSERT), supprimer (DELETE) ou modifier des données (UPDATE)</param>
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