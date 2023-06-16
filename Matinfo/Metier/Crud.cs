

using System.Collections.ObjectModel;

namespace Matinfo.Metier
{
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