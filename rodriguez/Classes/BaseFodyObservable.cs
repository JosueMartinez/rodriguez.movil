using System.ComponentModel;

namespace rodriguez.Classes
{
    public abstract class BaseFodyObservable : INotifyPropertyChanged
    {        
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
