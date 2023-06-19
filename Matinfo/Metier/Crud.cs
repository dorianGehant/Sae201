

using System.Collections.ObjectModel;

namespace Matinfo.Metier
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
    public interface Crud<T>
   {
       
        bool Create();
      
      bool Read();
      
      bool Update();
      
      void Delete();
      
      ObservableCollection<T> FindAll();
      
      ObservableCollection<T> FindBySelection(string criteres);
   
   }
}