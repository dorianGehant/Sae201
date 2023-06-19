

using System.Collections.ObjectModel;

namespace Matinfo.Metier
{
    public interface Crud<T>
   {
        /// <summary>
        /// Interfaces pour les m�thodes 
        /// de cr�ation
        /// de verification et voir
        /// de modification
        /// de suppr�ssion
        /// de recherches
        /// de recherches par s�lection
        /// </summary>
        bool Create();
      
      bool Read();
      
      bool Update();
      
      void Delete();
      
      ObservableCollection<T> FindAll();
      
      ObservableCollection<T> FindBySelection(string criteres);
   
   }
}