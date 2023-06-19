

using System.Collections.ObjectModel;

namespace Matinfo.Metier
{
    /// <summary>
    /// Interfaces pour les methodes 
    /// de creation
    /// de verification et voir
    /// de modification
    /// de suppression
    /// de recherches
    /// de recherches par selection
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