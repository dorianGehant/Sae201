

using System.Collections.ObjectModel;

namespace Matinfo.Metier
{
    public interface Crud<T>
   {
        /// <summary>
        /// Interfaces pour les méthodes 
        /// de création
        /// de verification et voir
        /// de modification
        /// de suppréssion
        /// de recherches
        /// de recherches par sélection
        /// </summary>
        bool Create();
      
      bool Read();
      
      bool Update();
      
      void Delete();
      
      ObservableCollection<T> FindAll();
      
      ObservableCollection<T> FindBySelection(string criteres);
   
   }
}