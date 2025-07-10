using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PrzedmiotBranzowy__Core.Helpers
{
    internal class MyObservableCollection<T>
    {
        private ObservableCollection<T> _collection;

        public MyObservableCollection()
        {
            _collection = new();
        }
    }
}
